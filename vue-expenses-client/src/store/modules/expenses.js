import Api from '@/services/api'
import { LOAD_EXPENSES, CREATE_EXPENSE, EDIT_EXPENSE, REMOVE_EXPENSE, ADD_ALERT, EDIT_STATISTICS } from '@/store/_actiontypes'
import { SET_EXPENSES, ADD_EXPENSE, UPDATE_EXPENSE, DELETE_EXPENSE } from '@/store/_mutationtypes'
import sumBy from 'lodash/sumBy'
import groupBy from 'lodash/groupBy'
import map from 'lodash/map'
import orderBy from 'lodash/orderBy'


const state = {
    expenses: []
};

const actions = {
    [LOAD_EXPENSES]({ commit }) {
        Api.get('/expenses')
            .then(response => {
                let expenses = response.data;
                commit(SET_EXPENSES, expenses);
            })
    },
    [CREATE_EXPENSE]({ commit, dispatch }, { expense }) {
        return Api.post('/expenses', {
            date: expense.date,
            categoryId: expense.categoryId,
            typeId: expense.typeId,
            value: expense.value,
            comments: expense.comments
        })
            .then(response => {
                let expense = response.data;
                commit(ADD_EXPENSE, expense);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense added successfully', color: 'success' }, { root: true });
                dispatch(`statistics/${EDIT_STATISTICS}`, { expense: expense, operation: 'create' }, { root: true });
            })
    },
    [EDIT_EXPENSE]({ commit, dispatch }, { expense }) {
        return Api.put(`/expenses/${expense.id}`, expense)
            .then(response => {
                let expense = response.data;
                commit(UPDATE_EXPENSE, expense);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense updaded successfully', color: 'success' }, { root: true });
                dispatch(`statistics/${EDIT_STATISTICS}`, { expense: expense, operation: 'edit' }, { root: true });
            })
    },
    [REMOVE_EXPENSE]({ commit, dispatch }, { id }) {
        return Api.delete(`/expenses/${id}`)
            .then(() => {
                var expense = state.expenses.filter(ec => ec.id == id)[0];
                commit(DELETE_EXPENSE, id);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense deleted successfully', color: 'success' }, { root: true });
                dispatch(`statistics/${EDIT_STATISTICS}`, { expense: expense, operation: 'remove' }, { root: true });
            })
    },
}

const mutations = {
    [SET_EXPENSES](state, expenses) {
        state.expenses = expenses;
    },
    [ADD_EXPENSE](state, expense) {
        state.expenses.push(expense);
    },
    [UPDATE_EXPENSE](state, expense) {
        let expenseUpdated = state.expenses.find(ec => ec.id == expense.id)
        expenseUpdated.date = expense.date;
        expenseUpdated.value = expense.value;
        expenseUpdated.categoryId = expense.categoryId;
        expenseUpdated.category = expense.category;
        expenseUpdated.typeId = expense.typeId;
        expenseUpdated.type = expense.type;
        expenseUpdated.comments = expense.comments;
    },
    [DELETE_EXPENSE](state, id) {
        state.expenses = state.expenses.filter(ec => ec.id != id)
    }
}

const getters = {
    overallSpent: state => {
        return new Intl.NumberFormat(window.navigator.language).format(sumBy(state.expenses, "value").toFixed(2))
    },
    mostSpentBy: state => {
        return orderBy(
            map(
                groupBy(state.expenses, (e) => {
                    return e.type
                }), (type, id) => ({
                    type: id,
                    value: sumBy(type, 'value')
                })), ['value'], ['desc'])[0].type;
    },
    mostSpentOn: state => {
        return orderBy(
            map(
                groupBy(state.expenses, (e) => {
                    return e.category
                }), (category, id) => ({
                    category: id,
                    value: sumBy(category, 'value')
                })), ['value'], ['desc'])[0].category;
    },
    spentThisYear: state => {
        var currentYear = new Date().getFullYear();
        return new Intl.NumberFormat(window.navigator.language).format(sumBy(
            state.expenses.filter((o) => {
                return new Date(o.date).getFullYear() == currentYear
            }), "value").toFixed(2))
    }
}

export const expenses = {
    namespaced: true,
    state,
    actions,
    mutations,
    getters
};