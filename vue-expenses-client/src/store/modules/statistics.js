import Api from '@/services/api'
import {
    LOAD_CATEGORIES_BREAKDOWN, LOAD_EXPENSES_BREAKDOWN, EDIT_STATISTICS, CREATE_NEWCATEGORY_STATISTICS, EDIT_CATEGORY_STATISTICS, DELETE_CATEGORY_STATISTICS
} from '@/store/_actiontypes'
import {
    SET_CATEGORIES_BREAKDOWN, SET_EXPENSES_BREAKDOWN, UPDATE_STATISTICS, ADD_NEWCATEGORY_STATISTICS, UPDATE_CATEGORY_STATISTICS, REMOVE_CATEGORY_STATISTICS
} from '@/store/_mutationtypes'
import map from 'lodash/map'
import sumBy from 'lodash/sumBy'
import groupBy from 'lodash/groupBy'
import forEach from 'lodash/forEach'
import uniq from 'lodash/uniq'

const state = {
    categorybreakdown: [],
    expensesbreakdown: []
};

const actions = {
    [LOAD_CATEGORIES_BREAKDOWN]({ commit }) {
        return Api.get('/statistics/getcurrentyearcategoriesbreakdown')
            .then(response => {
                commit(SET_CATEGORIES_BREAKDOWN, response.data);
            })
    },
    [LOAD_EXPENSES_BREAKDOWN]({ commit }) {
        return Api.get('/statistics/getcurrentyearexpensesbycategorybreakdown')
            .then(response => {
                commit(SET_EXPENSES_BREAKDOWN, response.data);
            })
    },
    [EDIT_STATISTICS]({ commit, dispatch }, { expense, operation }) {
        //when editing an expense (which is most likey not done often), reload the state
        if (operation === 'edit') {
            dispatch(LOAD_CATEGORIES_BREAKDOWN);
            dispatch(LOAD_EXPENSES_BREAKDOWN);
        } else {
            commit(UPDATE_STATISTICS, { expense, operation });
        }
    },
    [CREATE_NEWCATEGORY_STATISTICS]({ commit }, { category }) {
        commit(ADD_NEWCATEGORY_STATISTICS, category);
    },
    [EDIT_CATEGORY_STATISTICS]({ commit }, { category }) {
        commit(UPDATE_CATEGORY_STATISTICS, category);
    },
    [DELETE_CATEGORY_STATISTICS]({ commit }, { categoryId }) {
        commit(REMOVE_CATEGORY_STATISTICS, categoryId);
    }
};

const mutations = {
    [SET_CATEGORIES_BREAKDOWN](state, categorybreakdown) {
        state.categorybreakdown = categorybreakdown;
    },
    [SET_EXPENSES_BREAKDOWN](state, expensesbreakdown) {
        state.expensesbreakdown = expensesbreakdown;
    },
    [UPDATE_STATISTICS](state, payload) {
        var currentmonth = new Date().getMonth() + 1;
        const expenseDate = new Date(payload.expense.date);
        const expenseMonth = expenseDate.getMonth() + 1;

        //if the expense is for current month
        if (expenseMonth == currentmonth) {
            //check if there is a entry for the current month for the category and update it, if not create a new entry
            var currentmonthData = state.categorybreakdown.filter((o) => { return o.month == currentmonth; })
            var category = currentmonthData.filter((o) => { return o.name == payload.expense.category })
            if (category[0]) {
                if (payload.operation === 'create') {
                    category[0].spent += payload.expense.value;
                }
                else {
                    category[0].spent -= payload.expense.value;
                }
            } else if (payload.operation === 'create') {
                state.categorybreakdown.push({
                    budget: payload.expense.categoryBudget,
                    colour: payload.expense.categoryColour,
                    id: payload.expense.categoryId,
                    month: currentmonth,
                    name: payload.expense.category,
                    spent: payload.expense.value,

                })
            }

        }

        //check if there is a entry for the current category and month and update it, if not create a new entry
        var expensebreakdown = state.expensesbreakdown.filter((o) => { return o.month == expenseMonth && o.categoryName == payload.expense.category; });
        if (expensebreakdown[0]) {
            if (payload.operation === 'create') {
                expensebreakdown[0].spent += payload.expense.value;
            }
            else {
                expensebreakdown[0].spent -= payload.expense.value;
            }
        } else if (expenseDate.getYear() == new Date().getYear() && payload.operation === 'create') {
            state.expensesbreakdown.push({
                categoryColour: payload.expense.categoryColour,
                categoryName: payload.expense.category,
                month: expenseMonth,
                spent: payload.expense.value,

            })
        }
    },
    [ADD_NEWCATEGORY_STATISTICS](state, category) {
        var months = uniq(state.categorybreakdown.map(c => c.month));
        forEach(months, (value, key) => {
            state.categorybreakdown.push({
                budget: category.budget,
                colour: category.colourHex,
                id: category.id,
                month: value,
                name: category.name,
                spent: 0,

            })
        });

    },
    [UPDATE_CATEGORY_STATISTICS](state, category) {
        var categories = state.categorybreakdown.filter((o) => { return o.id == category.id });
        forEach(categories, (value, key) => {
            value.name = category.name,
                value.budget = category.budget,
                value.colour = category.colourHex
        });

        var expenses = state.expensesbreakdown.filter((o) => { return o.id == category.id });
        forEach(expenses, (value, key) => {
            value.categoryName = category.name,
                value.categoryColour = category.colourHex
        });

    },
    [REMOVE_CATEGORY_STATISTICS](state, categoryId) {
        state.categorybreakdown = state.categorybreakdown.filter((o) => { return o.id != categoryId });
        state.expensesbreakdown = state.expensesbreakdown.filter((o) => { return o.id != categoryId });
    }
}

const getters = {
    monthlyBudget: (state, getters, rootState) => {
        var currentmonth = new Date().getMonth() + 1;
        var currentMonthBudget = state.categorybreakdown.filter((o) => { return o.month == currentmonth; });
        var totalBudget = sumBy(rootState.expenseCategories.categories, (ec) => { return ec.budget });
        var totalSpent = sumBy(currentMonthBudget, (cm) => { return cm.spent });
        var remaining = totalBudget - totalSpent;
        return {
            data: [
                { value: totalSpent.toFixed(2), name: "Spent", itemStyle: { color: "#2779bd" } },
                { value: (remaining < 0 ? 0 : remaining).toFixed(2), name: "Remaining", itemStyle: { color: "#BDBDBD" } }
            ],
            totalBudget: new Intl.NumberFormat(window.navigator.language).format(totalBudget.toFixed(2)),
            totalSpent: new Intl.NumberFormat(window.navigator.language).format(totalSpent.toFixed(2))
        }
    },
    monthlyBudgetsByCategory: state => {
        var currentmonth = new Date().getMonth() + 1;
        var currentMonthBudgets = state.categorybreakdown.filter((o) => { return o.month == currentmonth; });
        var groupedBugetsByCategory = groupBy(currentMonthBudgets, (e) => { return e.name + '|' + e.colour });
        return map(groupedBugetsByCategory, function (budget, key) {
            let remaining = sumBy(budget, "budget") - sumBy(budget, "spent");
            return {
                name: key.split('|')[0],
                colour: key.split('|')[1],
                monthlyBudget: [
                    { value: sumBy(budget, "spent").toFixed(2), name: "Spent", itemStyle: { color: key.split('|')[1] } },
                    { value: (remaining < 0 ? 0 : remaining).toFixed(2), name: "Remaining", itemStyle: { color: "#BDBDBD" } }
                ]
            }
        });
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
