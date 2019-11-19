import Api from '@/services/api'
import { LOAD_CATEGORIES_BREAKDOWN, LOAD_EXPENSES_BREAKDOWN } from '@/store/_actiontypes'
import { SET_CATEGORIES_BREAKDOWN, SET_EXPENSES_BREAKDOWN } from '@/store/_mutationtypes'
import map from 'lodash/map'
import sumBy from 'lodash/sumBy'
import groupBy from 'lodash/groupBy'
import forEach from 'lodash/forEach'

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
    monthlyBudget: (state, getters, rootState) => {
        var currentmonth = new Date().getMonth() + 1;
        var currentMonthBudget = state.categorybreakdown.filter((o) => { return o.month == currentmonth; });
        var totalBudget = sumBy(rootState.expenseCategories.categories, (ec) => { return ec.budget });
        var totalSpent = sumBy(currentMonthBudget, (cm) => { return cm.spent });

        return [
            { value: totalSpent.toFixed(2), name: "Spent", itemStyle: { color: "#2779bd" } },
            { value: (totalBudget - totalSpent).toFixed(2), name: "Remaining", itemStyle: { color: "#BDBDBD" } }
        ]
    },
    monthlyBudgetsByCategory: state => {
        var currentmonth = new Date().getMonth() + 1;
        var currentMonthBudgets = state.categorybreakdown.filter((o) => { return o.month == currentmonth; });
        var groupedBugetsByCategory = groupBy(currentMonthBudgets, (e) => { return e.name + '|' + e.colour });
        return map(groupedBugetsByCategory, (budget, key) => ({
            name: key.split('|')[0],
            colour: key.split('|')[1],
            monthlyBudget: [
                { value: sumBy(budget, "spent").toFixed(2), name: "Spent", itemStyle: { color: key.split('|')[1] } },
                { value: (sumBy(budget, "budget") - sumBy(budget, "spent")).toFixed(2), name: "Remaining", itemStyle: { color: "#BDBDBD" } }
            ]
        }));
    },
    yearlyExpenses: state => {
        var months = ["Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec"];

        var groupedByMonths = groupBy(state.expensesbreakdown, (e) => { return e.month });
        var yearlyExpenses = {
            xAxisData: [],
            data: []
        };

        forEach(groupedByMonths, (value, key) => {
            yearlyExpenses.xAxisData.push(months[Number(key) - 1]);
            yearlyExpenses.data.push(sumBy(value, "spent").toFixed(0));
        });
        return yearlyExpenses;
    },
    categoryExpenses: state => {
        var categoryExpenses = [];
        var groupedByCategory = groupBy(state.expensesbreakdown, (e) => { return e.categoryName + '|' + e.categoryColour });

        forEach(groupedByCategory, (value, key) => {
            categoryExpenses.push({
                value: sumBy(value, "spent").toFixed(2),
                name: key.split('|')[0],
                itemStyle: { color: key.split('|')[1] }
            })
        });
        return categoryExpenses;
    }
}

export const statistics = {
    namespaced: true,
    state,
    actions,
    mutations,
    getters
};
