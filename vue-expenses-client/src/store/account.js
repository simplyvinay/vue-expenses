import { LOGIN_REQUEST, LOGIN_SUCCESS, LOGIN_FAILURE, LOGOUT_USER } from '@/store/mutationTypes'
import { LOGIN, LOGOUT, SET_ALERT } from '@/store/actionTypes'
import router from '@/router/index';
import Api from '@/services/api'

const state = {
    user: null,
    status: {}
};

const actions = {
    [LOGIN]({ dispatch, commit }, { email, password }) {
        commit(LOGIN_REQUEST, { email });

        Api.post('/login', {
            email,
            password
        })
            .then(response => {
                let user = response.data;
                commit(LOGIN_SUCCESS, user);
                router.push('/dashboard');
            })
            .catch((e) => {
                var error = e.response ? e.response.data.errors.Error : e;
                commit(LOGIN_FAILURE, error);
                dispatch(`alert/${SET_ALERT}`, { message: error, color: 'error' }, { root: true });
            });
    },
    [LOGOUT]({ commit }) {
        commit(LOGOUT_USER);
    }
};

const mutations = {
    [LOGIN_REQUEST](state) {
        state.status = { loggingIn: true };
    },
    [LOGIN_SUCCESS](state, user) {
        // login successful if there's a jwt token in the response
        if (user.token) {
            // store user details and jwt token in local storage
            localStorage.setItem('user', JSON.stringify(user));
        }
        state.status = { loggedIn: true };
        state.user = user;
    },
    [LOGIN_FAILURE](state) {
        state.status = {};
        state.user = null;
    },
    [LOGOUT_USER](state) {
        // remove user from local storage 
        localStorage.removeItem('user');
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