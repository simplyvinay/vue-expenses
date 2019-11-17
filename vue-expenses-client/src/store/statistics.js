import Api from '@/services/api'
import { TOGGLE_LOADING } from '@/store/_actionTypes'
import { SET_CATEGORIES_BREAKDOWN, SET_EXPENSES_BREAKDOWN } from '@/store/_mutationTypes'

const state = {
    monthlybudget: {},
    categorybreakdown: [],
    expensesbreakdown: []
};

const actions = {
    [LOAD_EXPENSE_TYPES]({ commit }) {
        Api.get('/expensetypes')
            .then(response => {
                let expenseTypes = response.data;
                commit(SET_EXPENSE_TYPE, expenseTypes);
            })
    }
};

export const statistics = {
    namespaced: true,
    state,
    actions,
    mutations
};
