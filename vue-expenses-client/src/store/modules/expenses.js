import Api from '@/services/api'
import { LOAD_EXPENSES, CREATE_EXPENSE, EDIT_EXPENSE, REMOVE_EXPENSE, ADD_ALERT, EDIT_STATISTICS, REMOVE_EXPENSESOFTYPE, REMOVE_EXPENSESOFCATEGORY } from '@/store/_actiontypes'
import { SET_EXPENSES, ADD_EXPENSE, UPDATE_EXPENSE, DELETE_EXPENSE, DELETE_EXPENSESOFTYPE, DELETE_EXPENSESOFCATEGORY } from '@/store/_mutationtypes'
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
    [REMOVE_EXPENSESOFTYPE]({commit}, { typeId }){
        commit(DELETE_EXPENSESOFTYPE, typeId);
    },
    [REMOVE_EXPENSESOFCATEGORY]({commit}, { categoryId }){
        commit(DELETE_EXPENSESOFCATEGORY, categoryId);
    }
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
    },
    [DELETE_EXPENSESOFTYPE](state, typeId){
        state.expenses = state.expenses.filter(ec => ec.typeId != typeId)
    },
    [DELETE_EXPENSESOFCATEGORY](state, categoryId){
        state.expenses = state.expenses.filter(ec => ec.categoryId != categoryId)
    }
}

const getters = {
    overallSpent: (state, getters, rootState) => {
        var overallSpent = new Intl.NumberFormat(window.navigator.language).format(sumBy(state.expenses, "value").toFixed(2))
        return `${rootState.account.user.displayCurrency} ${overallSpent}`
    },
    mostSpentBy: state => {
        return state.expenses.length <= 0 ? 'N/A' : orderBy(
            map(
                groupBy(state.expenses, (e) => {
                    return e.type
                }), (type, id) => ({
                    type: id,
                    value: sumBy(type, 'value')
                })), ['value'], ['desc'])[0].type;
    },
    mostSpentOn: state => {
        return state.expenses.length <= 0 ? 'N/A' : orderBy(
            map(
                groupBy(state.expenses, (e) => {
                    return e.category
                }), (category, id) => ({
                    category: id,
                    value: sumBy(category, 'value')
                })), ['value'], ['desc'])[0].category;
    },
    spentThisYear: (state, getters, rootState) => {
        var currentYear = new Date().getFullYear();
        var spentThisYear = new Intl.NumberFormat(window.navigator.language).format(sumBy(state.expenses.filter((o) => {
            return new Date(o.date).getFullYear() == currentYear;
        }), "value").toFixed(2))
        return `${rootState.account.user.displayCurrency} ${spentThisYear}`
    }
}

export const expenses = {
    namespaced: true,
    state,
    actions,
    mutations,
    getters
};