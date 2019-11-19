import Api from '@/services/api'
import { LOAD_EXPENSES, ADD_EXPENSE, EDIT_EXPENSE, REMOVE_EXPENSE, ADD_ALERT } from '@/store/_actiontypes'
import { SET_EXPENSES, CREATE_EXPENSE, UPDATE_EXPENSE, DELETE_EXPENSE } from '@/store/_mutationtypes'

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
    [ADD_EXPENSE]({ commit, dispatch }, { expense, onSuccess }) {
        Api.post('/expenses', {
            date: expense.expenseDate,
            categoryId: expense.expense,
            typeId: expense.expenseType,
            value: expense.expenseAmount,
            comments: expense.expenseComments
        })
            .then(response => {
                let expense = response.data;
                commit(CREATE_EXPENSE, expense);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense added successfully', color: 'success' }, { root: true });
                onSuccess();
            })
    },
    [EDIT_EXPENSE]({ commit, dispatch }, { expense, onSuccess }) {
        Api.put(`/expenses/${expense.id}`, expense)
            .then(response => {
                let expense = response.data;
                commit(UPDATE_EXPENSE, expense);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense updaded successfully', color: 'success' }, { root: true });
                onSuccess();
            })
    },
    [REMOVE_EXPENSE]({ commit, dispatch }, { id }) {
        Api.delete(`/expenses/${id}`)
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
        state.expenses = state.expenses.concat(expense);
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

export const expense = {
    namespaced: true,
    state,
    actions,
    mutations
};