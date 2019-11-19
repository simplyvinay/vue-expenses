import { TOGGLE_LOADING } from '@/store/_actiontypes'
import { UPDATE_LOADING } from '@/store/_mutationtypes'

const state = {
    loading: false
};

const actions = {
    [TOGGLE_LOADING]({ commit }, { loading }) {
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
