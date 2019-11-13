import Api from '@/services/api'
import { GET_EXPENSE_TYPES, ADD_EXPENSE_TYPE, EDIT_EXPENSE_TYPE, REMOVE_EXPENSE_TYPE, SET_ALERT } from '@/store/_actionTypes'
import { SET_EXPENSE_TYPE, CREATE_EXPENSE_TYPE, UPDATE_EXPENSE_TYPE, DELETE_EXPENSE_TYPE } from '@/store/_mutationTypes'

const state = {
    expensetypes: []
};

const actions = {
    [GET_EXPENSE_TYPES]({ commit }) {
        Api.get('/expensetypes')
            .then(response => {
                let expenseTypes = response.data;
                commit(SET_EXPENSE_TYPE, expenseTypes);
            })
    },
    [ADD_EXPENSE_TYPE]({ commit, dispatch }, { expenseType, onSuccess }) {
        Api.post('/expensetypes', {
            name: expenseType.name,
            description: expenseType.description
        })
            .then(response => {
                let expenseType = response.data;
                commit(CREATE_EXPENSE_TYPE, expenseType);
                dispatch(`alert/${SET_ALERT}`, { message: 'Expense type added successfully', color: 'success' }, { root: true });
                onSuccess();
            })
    },
    [EDIT_EXPENSE_TYPE]({ commit, dispatch }, { expenseType, onSuccess }) {
        Api.put(`/expensetypes/${expenseType.id}`, expenseType)
            .then(response => {
                let expenseType = response.data;
                commit(UPDATE_EXPENSE_TYPE, expenseType);
                dispatch(`alert/${SET_ALERT}`, { message: 'Expense type updaded successfully', color: 'success' }, { root: true });
                onSuccess();
            })
    },
    [REMOVE_EXPENSE_TYPE]({ commit, dispatch }, { id }) {
        Api.delete(`/expensetypes/${id}`)
            .then(response => {
                commit(DELETE_EXPENSE_TYPE, id);
                dispatch(`alert/${SET_ALERT}`, { message: 'Expense type deleted successfully', color: 'success' }, { root: true });
            })
    },
}

const mutations = {
    [SET_EXPENSE_TYPE](state, expenseTypes) {
        state.expensetypes = expenseTypes;
    },
    [CREATE_EXPENSE_TYPE](state, expenseType) {
        state.expensetypes = state.expensetypes.concat(expenseType);
    },
    [UPDATE_EXPENSE_TYPE](state, expenseType) {
        let expenseTypeUpdated = state.expensetypes.find(et => et.id == expenseType.id)
        expenseTypeUpdated.name = expenseType.name;
        expenseTypeUpdated.description = expenseType.description;
    },
    [DELETE_EXPENSE_TYPE](state, id) {
        state.expensetypes = state.expensetypes.filter(et => et.id != id)
    }
}

export const expensetype = {
    namespaced: true,
    state,
    actions,
    mutations
};