import Api from '@/services/api'
import { LOAD_CATEGORIES, CREATE_CATEGORY, EDIT_CATEGORY, REMOVE_CATEGORY, ADD_ALERT } from '@/store/_actiontypes'
import { SET_CATEGORIES, ADD_CATEGORY, UPDATE_CATEGORY, DELETE_CATEGORY } from '@/store/_mutationtypes'

const state = {
    categories: []
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
    [SET_CATEGORIES](state, expenseCategories) {
        state.categories = expenseCategories;
    },
    [ADD_CATEGORY](state, expenseCategory) {
        state.categories = state.categories.concat(expenseCategory);
    },
    [UPDATE_CATEGORY](state, expenseCategory) {
        let expenseCategoryUpdated = state.categories.find(ec => ec.id == expenseCategory.id)
        expenseCategoryUpdated.name = expenseCategory.name;
        expenseCategoryUpdated.description = expenseCategory.description;
        expenseCategoryUpdated.budget = expenseCategory.budget;
        expenseCategoryUpdated.colourHex = expenseCategory.colourHex;
    },
    [DELETE_CATEGORY](state, id) {
        state.categories = state.categories.filter(ec => ec.id != id)
    }
}

export const expenseCategories = {
    namespaced: true,
    state,
    actions,
    mutations
};