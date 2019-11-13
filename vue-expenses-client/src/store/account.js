import Api from '@/services/api'
import router from '@/router/index';
import { LOGIN, LOGOUT } from '@/store/_actionTypes'
import { LOGIN_SUCCESS, LOGIN_FAILURE, LOGOUT_USER } from '@/store/_mutationTypes'

const state = {
    user: null
};

const actions = {
    [LOGIN]({ commit }, { email, password }) {
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
            });
    },
    [LOGOUT]({ commit }) {
        commit(LOGOUT_USER);
    }
};

const mutations = {
    [LOGIN_SUCCESS](state, user) {
        // login successful if there's a jwt token in the response
        if (user.token) {
            // store user details and jwt token in local storage
            localStorage.setItem('user', JSON.stringify(user));
        }
        state.user = user;
    },
    [LOGIN_FAILURE](state) {
        state.user = null;
    },
    [LOGOUT_USER](state) {
        // remove user from local storage 
        localStorage.removeItem('user');
        state.user = null;
    }
};

export const account = {
    namespaced: true,
    state,
    actions,
    mutations
};