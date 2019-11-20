import Api from '@/services/api'
import { LOAD_EXPENSES, CREATE_EXPENSE, EDIT_EXPENSE, REMOVE_EXPENSE, ADD_ALERT, EDIT_STATISTICS } from '@/store/_actiontypes'
import { SET_EXPENSES, ADD_EXPENSE, UPDATE_EXPENSE, DELETE_EXPENSE } from '@/store/_mutationtypes'

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
                dispatch(`statistics/${EDIT_STATISTICS}`, { expense: expense }, { root: true });
            })
    },
    [EDIT_EXPENSE]({ commit, dispatch }, { expense }) {
        return Api.put(`/expenses/${expense.id}`, expense)
            .then(response => {
                let expense = response.data;
                commit(UPDATE_EXPENSE, expense);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense updaded successfully', color: 'success' }, { root: true });
            })
    },
    [REMOVE_EXPENSE]({ commit, dispatch }, { id }) {
        return Api.delete(`/expenses/${id}`)
            .then(() => {
                commit(DELETE_EXPENSE, id);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense deleted successfully', color: 'success' }, { root: true });
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

export const expenses = {
    namespaced: true,
    state,
    actions,
    mutations
};