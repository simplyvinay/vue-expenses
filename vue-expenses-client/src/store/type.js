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
    [ADD_TYPE]({ commit }, { expenseType }) {
        Api.post('/expensetypes', {
            name: expenseType.name,
            description: expenseType.description
        })
            .then(response => {
                let expenseType = response.data;
                commit(CREATE_TYPE, expenseType);
            })
    },
    [EDIT_TYPE]({ commit }, { expenseType }) {
        Api.put(`/expensetypes/${expenseType.id}`, expenseType)
        .then(response => {
            let expenseType = response.data;
            commit(UPDATE_TYPE, expenseType);
        })        
    },
    [REMOVE_TYPE]({ commit }, { id }) {
        Api.delete(`/expensetypes/${ id }`)
        .then(response => {
            commit(DELETE_TYPE, id);
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
    [UPDATE_TYPE](state, expenseType) {
        let expenseTypeUpdated = state.types.find(et => et.id == expenseType.id)
        expenseTypeUpdated.name = expenseType.name;
        expenseTypeUpdated.description = expenseType.description;
    },
    [DELETE_TYPE](state, id) {
        state.types = state.types.filter(et => et.id != id)
      },
}

export const type = {
    namespaced: true,
    state,
    actions,
    mutations
};