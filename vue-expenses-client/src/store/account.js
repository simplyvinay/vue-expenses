import { LOGIN_REQUEST, LOGIN_SUCCESS, LOGIN_FAILURE, LOGOUT_USER } from '@/store/mutationTypes'
import { LOGIN, LOGOUT, SET_ALERT } from '@/store/actionTypes'
import router from '@/router/index';
import Api from '@/services/apiService'

const state = {
    user: null,
    status: {}
};

const actions = {
    async [LOGIN]({ commit, dispatch }, { email, password }) {
        try {
            commit(LOGIN_REQUEST, { email });
            
            let response = await Api().post('/login', {
                email,
                password
            });
            let user = response.data;
            
            commit(LOGIN_SUCCESS, user);
            router.push('/');
        } catch (e) {
            var error = e.response ? e.response.data.errors.Error : e;
            console.log(error);
            commit(LOGIN_FAILURE, error);
            dispatch(`alert/${SET_ALERT}`, { message: error, color: 'error' }, { root: true });
        }
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
            // store user details and jwt token in local storage to keep user logged in between page refreshes
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