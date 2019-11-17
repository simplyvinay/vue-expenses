import { ADD_ALERT } from '@/store/_actionTypes'
import { CREATE_ALERT } from '@/store/_mutationTypes'

const state = {
    alert: {}
};

const actions = {
    [ADD_ALERT]({commit}, alert) {
        alert.show = true;
        alert.color = alert.color;
        alert.message = alert.message;
        commit(CREATE_ALERT, alert);
      }
};

const mutations = {
    [CREATE_ALERT](state, alert) {
        state.alert = alert;
      }
};

export const alert = {
    namespaced: true,
    state,
    actions,
    mutations
};
