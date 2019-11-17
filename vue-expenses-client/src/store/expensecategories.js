import Api from '@/services/api'
import { LOAD_CATEGORIES, CREATE_CATEGORY, EDIT_CATEGORY, REMOVE_CATEGORY, ADD_ALERT } from '@/store/_actionTypes'
import { SET_CATEGORIES, ADD_CATEGORY, UPDATE_CATEGORY, DELETE_CATEGORY } from '@/store/_mutationTypes'

const state = {
    expensecategories: []
};

const actions = {
    [LOAD_CATEGORIES]({ commit }) {
        Api.get('/expensecategories')
            .then(response => {
                let expensecategories = response.data;
                commit(SET_CATEGORIES, expensecategories);
            })
    },
    [CREATE_CATEGORY]({ commit, dispatch }, { expenseCategory, onSuccess }) {
        Api.post('/expensecategories', {
            name: expenseCategory.name,
            description: expenseCategory.description,
            budget: expenseCategory.budget,
            colourhex: expenseCategory.colourHex
        })
            .then(response => {
                let expenseCategory = response.data;
                commit(ADD_CATEGORY, expenseCategory);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense category added successfully', color: 'success' }, { root: true });
                onSuccess();
            })
    },
    [EDIT_CATEGORY]({ commit, dispatch }, { expenseCategory, onSuccess }) {
        Api.put(`/expensecategories/${expenseCategory.id}`, expenseCategory)
            .then(response => {
                let expenseCategory = response.data;
                commit(UPDATE_CATEGORY, expenseCategory);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense category updaded successfully', color: 'success' }, { root: true });
                onSuccess();
            })
    },
    [REMOVE_CATEGORY]({ commit, dispatch }, { id }) {
        Api.delete(`/expensecategories/${id}`)
            .then(response => {
                commit(DELETE_CATEGORY, id);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense category deleted successfully', color: 'success' }, { root: true });
            })
    },
}

const mutations = {
    [SET_CATEGORIES](state, expensecategories) {
        state.expensecategories = expensecategories;
    },
    [ADD_CATEGORY](state, expenseCategory) {
        state.expensecategories = state.expensecategories.concat(expenseCategory);
    },
    [UPDATE_CATEGORY](state, expenseCategory) {
        let expenseCategoryUpdated = state.expensecategories.find(ec => ec.id == expenseCategory.id)
        expenseCategoryUpdated.name = expenseCategory.name;
        expenseCategoryUpdated.description = expenseCategory.description;
        expenseCategoryUpdated.budget = expenseCategory.budget;
        expenseCategoryUpdated.colourHex = expenseCategory.colourHex;
    },
    [DELETE_CATEGORY](state, id) {
        state.expensecategories = state.expensecategories.filter(ec => ec.id != id)
    }
}

export const expensecategories = {
    namespaced: true,
    state,
    actions,
    mutations
};