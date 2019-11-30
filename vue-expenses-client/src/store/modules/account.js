import Api from '@/services/api'
import router from '@/router/index';
import { LOGIN, LOGOUT, REFRESHTOKEN, EDIT_USER_DETAILS, EDIT_USER_SETTINGS, EDIT_USER_PROFILE, ADD_ALERT } from '@/store/_actiontypes'
import { LOGIN_SUCCESS, LOGIN_FAILURE, LOGOUT_USER, UPDATE_USER_DETAILS, UPDATE_USER_SETTINGS, UPDATE_USER_PROFILE } from '@/store/_mutationtypes'

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
            .catch(() => {
                commit(LOGIN_FAILURE);
            });
    },
    [REFRESHTOKEN]({ commit }, { refreshtoken, token }) {
        return Api.post('/refreshtoken', {
            token,
            refreshtoken
        })
            .then(response => {
                let user = response.data;
                commit(LOGIN_SUCCESS, user);
            })
            .catch((e) => {
                commit(LOGIN_FAILURE);
            });
    },
    [LOGOUT]({ commit }) {
        commit(LOGOUT_USER);
    },
    [EDIT_USER_DETAILS]({ commit }) {
        commit(UPDATE_USER_DETAILS);
    },
    [EDIT_USER_SETTINGS]({ commit, dispatch }, { systemName, displayCurrency, useDarkMode }) {
        return Api.post('/settings', {
            systemName,
            displayCurrency,
            useDarkMode
        })
            .then(response => {
                commit(UPDATE_USER_SETTINGS, response.data);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Settings updaded successfully', color: 'success' }, { root: true });
            })
    },
    [EDIT_USER_PROFILE]({ commit, dispatch }, { firstName, lastName }) {
        return Api.post('/profile', {
            firstName,
            lastName
        })
            .then(response => {
                commit(UPDATE_USER_PROFILE, response.data);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Profile updaded successfully', color: 'success' }, { root: true });
            })
    }
};

const mutations = {
    [LOGIN_SUCCESS](state, user) {
        // login successful if there's a jwt token in the response
        if (user.token) {
            // store user details and jwt token in local storage
            localStorage.setItem('user', JSON.stringify({
                token: user.token,
                refreshToken: user.refreshToken
            }));
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
    },
    [UPDATE_USER_DETAILS](state) {
        state.user.useDarkMode = !state.user.useDarkMode;
    },
    [UPDATE_USER_SETTINGS](state, { systemName, displayCurrency, useDarkMode, theme }) {
        state.user.systemName = systemName;
        state.user.useDarkMode = useDarkMode;
        state.user.theme = theme;
    },
    [UPDATE_USER_PROFILE](state, { firstName, lastName, fullName }) {
        state.user.firstName = firstName;
        state.user.lastName = lastName;
        state.user.fullName = fullName;
    },
};

const getters = {
    userNameInitials: (state) => {
        var initials = state.user.fullName.match(/\b\w/g) || [];
        return ((initials.shift() || "") + (initials.pop() || "")).toUpperCase();
      }
}

export const account = {
    namespaced: true,
    state,
    actions,
    mutations,
    getters
};