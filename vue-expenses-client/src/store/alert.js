import { ADD_ALERT } from '@/store/mutationTypes'
import { SET_ALERT } from '@/store/actionTypes'

const state = {
    alerts: []
};

const actions = {
    [SET_ALERT]({commit}, alert) {
        alert.show = true;
        alert.color = alert.color;
        alert.message = alert.message;
        commit(ADD_ALERT, alert);
      }
};

const mutations = {
    [ADD_ALERT](state, alert) {
        state.alerts = state.alerts.concat(alert);
      }
};

export const alert = {
    namespaced: true,
    state,
    actions,
    mutations
};
