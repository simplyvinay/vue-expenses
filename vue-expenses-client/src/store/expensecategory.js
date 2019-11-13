import Api from '@/services/api'
import { GET_CATEGORIES, ADD_CATEGORY, EDIT_CATEGORY, REMOVE_CATEGORY, SET_ALERT } from '@/store/_actionTypes'
import { SET_CATEGORIES, CREATE_CATEGORY, UPDATE_CATEGORY, DELETE_CATEGORY } from '@/store/_mutationTypes'

const state = {
    expensecategories: []
};

const actions = {
    [GET_CATEGORIES]({ commit }) {
        Api.get('/expensecategories')
            .then(response => {
                let expensecategories = response.data;
                commit(SET_CATEGORIES, expensecategories);
            })
    },
    [ADD_CATEGORY]({ commit, dispatch }, { expenseCategory, onSuccess }) {
        Api.post('/expensecategories', {
            name: expenseCategory.name,
            description: expenseCategory.description,
            budget: expenseCategory.budget,
            colourhex: expenseCategory.colourHex
        })
            .then(response => {
                let expenseCategory = response.data;
                commit(CREATE_CATEGORY, expenseCategory);
                dispatch(`alert/${SET_ALERT}`, { message: 'Expense category added successfully', color: 'success' }, { root: true });
                onSuccess();
            })
    },
    [EDIT_CATEGORY]({ commit, dispatch }, { expenseCategory, onSuccess }) {
        Api.put(`/expensecategories/${expenseCategory.id}`, expenseCategory)
            .then(response => {
                let expenseCategory = response.data;
                commit(UPDATE_CATEGORY, expenseCategory);
                dispatch(`alert/${SET_ALERT}`, { message: 'Expense category updaded successfully', color: 'success' }, { root: true });
                onSuccess();
            })
    },
    [REMOVE_CATEGORY]({ commit, dispatch }, { id }) {
        Api.delete(`/expensecategories/${id}`)
            .then(response => {
                commit(DELETE_CATEGORY, id);
                dispatch(`alert/${SET_ALERT}`, { message: 'Expense category deleted successfully', color: 'success' }, { root: true });
            })
    },
}

const mutations = {
    [SET_CATEGORIES](state, expensecategories) {
        state.expensecategories = expensecategories;
    },
    [CREATE_CATEGORY](state, expenseCategory) {
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

export const expensecategory = {
    namespaced: true,
    state,
    actions,
    mutations
};