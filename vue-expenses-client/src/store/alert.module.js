const state = {
    alerts: []
};

const actions = {
    setAlert({commit}, alert) {
        alert.show = true;
        alert.color = alert.color;
        alert.message = alert.message;
        commit('SET_ALERT', alert);
      }
};

const mutations = {
    SET_ALERT(state, alert) {
        state.alerts = state.alerts.concat(alert);
      }
};

export const alert = {
    namespaced: true,
    state,
    actions,
    mutations
};
