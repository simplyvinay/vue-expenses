import Api from '@/services/api'
import { LOAD_CATEGORIES, CREATE_CATEGORY, EDIT_CATEGORY, REMOVE_CATEGORY, ADD_ALERT, CREATE_NEWCATEGORY_STATISTICS, EDIT_CATEGORY_STATISTICS, REMOVE_EXPENSESOFCATEGORY, LOAD_CATEGORIES_BREAKDOWN, LOAD_EXPENSES_BREAKDOWN } from '@/store/_actiontypes'
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
    [CREATE_CATEGORY]({ commit, dispatch }, { expenseCategory }) {
        return Api.post('/expensecategories', {
            name: expenseCategory.name,
            description: expenseCategory.description,
            budget: expenseCategory.budget,
            colourhex: expenseCategory.colourHex
        })
            .then(response => {
                let expenseCategory = response.data;
                commit(ADD_CATEGORY, expenseCategory);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense category added successfully', color: 'success' }, { root: true });
                dispatch(`statistics/${CREATE_NEWCATEGORY_STATISTICS}`, { category: expenseCategory }, { root: true });
            })
    },
    [EDIT_CATEGORY]({ commit, dispatch }, { expenseCategory }) {
        return Api.put(`/expensecategories/${expenseCategory.id}`, expenseCategory)
            .then(response => {
                let expenseCategory = response.data;
                commit(UPDATE_CATEGORY, expenseCategory);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense category updaded successfully', color: 'success' }, { root: true });
                dispatch(`statistics/${EDIT_CATEGORY_STATISTICS}`, { category: expenseCategory }, { root: true });
            });
    },
    [REMOVE_CATEGORY]({ commit, dispatch }, { id }) {
        Api.delete(`/expensecategories/${id}`)
            .then(() => {
                commit(DELETE_CATEGORY, id);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Expense category deleted successfully', color: 'success' }, { root: true });
                dispatch(`expenses/${REMOVE_EXPENSESOFCATEGORY}`, { categoryId: id }, { root: true });
                dispatch(`statistics/${LOAD_CATEGORIES_BREAKDOWN}`, {}, { root: true });
                dispatch(`statistics/${LOAD_EXPENSES_BREAKDOWN}`, {}, { root: true });
            })
    },
}

const mutations = {
    [SET_CATEGORIES](state, expenseCategories) {
        state.categories = expenseCategories;
    },
    [ADD_CATEGORY](state, expenseCategory) {
        state.categories.push(expenseCategory);
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