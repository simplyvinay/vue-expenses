import Api from '@/services/api'
import { LOAD_EXPENSE_TYPES, CREATE_EXPENSE_TYPE, EDIT_EXPENSE_TYPE, REMOVE_EXPENSE_TYPE, ADD_ALERT, REMOVE_EXPENSESOFTYPE, LOAD_CATEGORIES_BREAKDOWN, LOAD_EXPENSES_BREAKDOWN } from '@/store/_actiontypes'
import { SET_EXPENSE_TYPE, ADD_EXPENSE_TYPE, UPDATE_EXPENSE_TYPE, DELETE_EXPENSE_TYPE } from '@/store/_mutationtypes'

const state = {
    types: []
};

const actions = {
    [LOAD_EXPENSE_TYPES]({ commit }) {
        Api.get('/expensetypes')
            .then(response => {
                let expenseTypes = response.data;
                commit(SET_EXPENSE_TYPE, expenseTypes);
            })
    },
    [CREATE_EXPENSE_TYPE]({ commit, dispatch }, { expenseType }) {
        return Api.post('/expensetypes', {
            name: expenseType.name,
            description: expenseType.description
        })
            .then(response => {
                let expenseType = response.data;
                commit(ADD_EXPENSE_TYPE, expenseType);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense type added successfully', color: 'success' }, { root: true });
            })
    },
    [EDIT_EXPENSE_TYPE]({ commit, dispatch }, { expenseType }) {
        return Api.put(`/expensetypes/${expenseType.id}`, expenseType)
            .then(response => {
                let expenseType = response.data;
                commit(UPDATE_EXPENSE_TYPE, expenseType);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense type updaded successfully', color: 'success' }, { root: true });
            })
    },
    [REMOVE_EXPENSE_TYPE]({ commit, dispatch }, { id }) {
        return Api.delete(`/expensetypes/${id}`)
            .then(() => {
                commit(DELETE_EXPENSE_TYPE, id);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense type deleted successfully', color: 'success' }, { root: true });
                dispatch(`expenses/${REMOVE_EXPENSESOFTYPE}`, { typeId: id }, { root: true });
                dispatch(`statistics/${LOAD_CATEGORIES_BREAKDOWN}`, {}, { root: true });
                dispatch(`statistics/${LOAD_EXPENSES_BREAKDOWN}`, {}, { root: true });
            })
    },
}

const mutations = {
    [SET_EXPENSE_TYPE](state, expenseTypes) {
        state.types = expenseTypes;
    },
    [ADD_EXPENSE_TYPE](state, expenseType) {
        state.types.push(expenseType);
    },
    [UPDATE_EXPENSE_TYPE](state, expenseType) {
        let expenseTypeUpdated = state.types.find(et => et.id == expenseType.id)
        expenseTypeUpdated.name = expenseType.name;
        expenseTypeUpdated.description = expenseType.description;
    },
    [DELETE_EXPENSE_TYPE](state, id) {
        state.types = state.types.filter(et => et.id != id)
    }
}

export const expenseTypes = {
    namespaced: true,
    state,
    actions,
    mutations
};