import Api from '@/services/api'
import { LOAD_CATEGORIES_BREAKDOWN, LOAD_EXPENSES_BREAKDOWN } from '@/store/_actionTypes'
import { SET_CATEGORIES_BREAKDOWN, SET_EXPENSES_BREAKDOWN } from '@/store/_mutationTypes'
import map from 'lodash/map'
import sumBy from 'lodash/sumBy'
import groupBy from 'lodash/groupBy'


const state = {
    categorybreakdown: [],
    expensesbreakdown: []
};

const actions = {
    [LOAD_CATEGORIES_BREAKDOWN]({ commit }) {
        Api.get('/statistics/getcurrentyearcategoriesbreakdown')
            .then(response => {
                commit(SET_CATEGORIES_BREAKDOWN, response.data);
            })
    },
    [LOAD_EXPENSES_BREAKDOWN]({ commit }) {
        Api.get('/statistics/getcurrentyearexpensesbycategorybreakdown')
            .then(response => {
                commit(SET_EXPENSES_BREAKDOWN, response.data);
            })
    }
};

const mutations = {
    [SET_CATEGORIES_BREAKDOWN](state, categorybreakdown) {
        state.categorybreakdown = categorybreakdown;
    },
    [SET_EXPENSES_BREAKDOWN](state, expensesbreakdown) {
        state.expensesbreakdown = expensesbreakdown;
    },
}

const getters = {
    monthlybudget: state => {
        var currentmonth = new Date().getMonth() + 1;
        var currentMonthBudget = state.categorybreakdown.filter((o) => { return o.month == currentmonth; });
        var totalBudget = sumBy(currentMonthBudget, (cm) => { return cm.budget });
        var totalSpent = sumBy(currentMonthBudget, (cm) => { return cm.spent });

        return [
            { value: totalSpent.toFixed(2), name: "Spent" },
            { value: (totalBudget - totalSpent).toFixed(2), name: "Remaining" }
        ]
    },
    monthlyBudgetsByCategory: state => {
        var currentmonth = new Date().getMonth() + 1;
        var currentMonthBudgets = state.categorybreakdown.filter((o) => { return o.month == currentmonth; });
        var groupedBugetsByCategory = groupBy(currentMonthBudgets, (e) => { return e.name + '|' + e.colour })
        return map(groupedBugetsByCategory, (budget, key) => ({
            name: key.split('|')[0],
            colour: key.split('|')[1],
            monthlybudget: [
                { value: sumBy(budget, "spent").toFixed(2), name: "Spent" },
                { value: (sumBy(budget, "budget") - sumBy(budget, "spent")).toFixed(2), name: "Remaining" }
            ]
        }));
    }
}

export const statistics = {
    namespaced: true,
    state,
    actions,
    mutations,
    getters
};
