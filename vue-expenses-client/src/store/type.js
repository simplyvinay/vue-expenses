import Api from '@/services/api'
import { GET_TYPES, ADD_TYPE, EDIT_TYPE, REMOVE_TYPE } from '@/store/_actionTypes'
import { SET_TYPES, CREATE_TYPE, UPDATE_TYPE, DELETE_TYPE } from '@/store/_mutationTypes'

const state = {
    types: []
};

const actions = {
    [GET_TYPES]({ commit }) {
        Api.get('/expensetypes')
            .then(response => {
                let expenseTypes = response.data;
                commit(SET_TYPES, expenseTypes);
            })
    },
    [ADD_TYPE]({ commit }, { name, description }) {
        Api.post('/expensetypes', {
            name,
            description
        })
            .then(response => {
                let expenseType = response.data;
                commit(CREATE_TYPE, expenseType);
            })
    },
}

const mutations = {
    [SET_TYPES](state, expenseTypes) {
        state.types = expenseTypes;
    },
    [CREATE_TYPE](state, expenseType) {
        state.types = state.types.concat(expenseType);
    },
}

export const types = {
    namespaced: true,
    state,
    actions,
    mutations
};