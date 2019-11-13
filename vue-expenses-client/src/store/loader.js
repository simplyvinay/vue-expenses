
import { SET_LOADING } from '@/store/_actionTypes'
import { UPDATE_LOADING } from '@/store/_mutationTypes'

const state = {
    loading: false
};

const actions = {
    [SET_LOADING]({ commit }, { loading }) {
        commit(UPDATE_LOADING, loading);
    }
};

const mutations = {
    [UPDATE_LOADING](state, loading) {
        state.loading = loading;
    }
};

export const loader = {
    namespaced: true,
    state,
    actions,
    mutations
};
