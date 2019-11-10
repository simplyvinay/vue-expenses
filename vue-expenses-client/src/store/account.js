import { userService } from '@/services/user.service';
import { LOGIN_REQUEST, LOGIN_SUCCESS, LOGIN_FAILURE, LOGOUT_USER } from '@/store/mutationTypes'
import { LOGIN, LOGOUT, SET_ALERT } from '@/store/actionTypes'
import router from '@/router/index';

const user = JSON.parse(localStorage.getItem('user'));
const state = user
    ? { status: { loggedIn: true }, user }
    : { status: {}, user: null };

const actions = {
    [LOGIN]({ dispatch, commit }, { email, password }) {
        commit(LOGIN_REQUEST, { email });

        userService.login(email, password)
            .then(
                user => {
                    commit(LOGIN_SUCCESS, user);
                    router.push('/');
                },
                error => {
                    commit(LOGIN_FAILURE, error.message);
                    dispatch(`alert/${SET_ALERT}`, { message: error.message, color: 'error' }, { root: true });
                }
            );
    },
    [LOGOUT]({ commit }) {
        userService.logout();
        commit(LOGOUT_USER);
    }
};

const mutations = {
    [LOGIN_REQUEST](state, user) {
        state.status = { loggingIn: true };
        state.user = user;
    },
    [LOGIN_SUCCESS](state, user) {
        state.status = { loggedIn: true };
        state.user = user;
    },
    [LOGIN_FAILURE](state) {
        state.status = {};
        state.user = null;
    },
    [LOGOUT_USER](state) {
        state.status = {};
        state.user = null;
    }
};

export const account = {
    namespaced: true,
    state,
    actions,
    mutations
};