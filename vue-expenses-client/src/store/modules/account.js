import Api from '@/services/api'
import router from '@/router/index';
import { LOGIN, LOGOUT, REGISTER, REFRESHTOKEN, EDIT_USER_DETAILS, EDIT_USER_SETTINGS, EDIT_USER_PROFILE, ADD_ALERT, LOAD_CURRENCIES } from '@/store/_actiontypes'
import { LOGIN_SUCCESS, LOGIN_FAILURE, LOGOUT_USER, UPDATE_USER_DETAILS, UPDATE_USER_SETTINGS, UPDATE_USER_PROFILE, SET_CURRENCIES } from '@/store/_mutationtypes'

const state = {
    user: null,
    currencies: []
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
    [REGISTER]({ dispatch }, { email, firstName, lastName, password }) {
        Api.post('/register', {
            email, firstName, lastName, password
        })
            .then(() => {
                dispatch(`alert/${ADD_ALERT}`, { message: 'User registered successfully', color: 'success' }, { root: true });
                router.push('/login');
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
    [EDIT_USER_SETTINGS]({ commit, dispatch }, { systemName, currencyRegionName, useDarkMode }) {
        return Api.put('/settings', {
            systemName,
            currencyRegionName,
            useDarkMode
        })
            .then(response => {
                commit(UPDATE_USER_SETTINGS, response.data);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Settings updaded successfully', color: 'success' }, { root: true });
            })
    },
    [EDIT_USER_PROFILE]({ commit, dispatch }, { firstName, lastName }) {
        return Api.put('/profile', {
            firstName,
            lastName
        })
            .then(response => {
                commit(UPDATE_USER_PROFILE, response.data);
                dispatch(`alert/${ADD_ALERT}`, { message: 'Profile updaded successfully', color: 'success' }, { root: true });
            })
    },
    [LOAD_CURRENCIES]({ commit }) {
        Api.get('/currencies')
            .then(response => {
                commit(SET_CURRENCIES, response.data);
            })
    }
};

const mutations = {
    [LOGIN_SUCCESS](state, user) {
        state.user = user;
    },
    [LOGIN_FAILURE](state) {
        state.user = null;
    },
    [LOGOUT_USER](state) {
        state.user = null;
    },
    [UPDATE_USER_DETAILS](state) {
        state.user.useDarkMode = !state.user.useDarkMode;
    },
    [UPDATE_USER_SETTINGS](state, { systemName, currencyRegionName, useDarkMode, theme, displayCurrency }) {
        state.user.systemName = systemName;
        state.user.useDarkMode = useDarkMode;
        state.user.theme = theme;
        state.user.currencyRegionName = currencyRegionName;
        state.user.displayCurrency = displayCurrency;
    },
    [UPDATE_USER_PROFILE](state, { firstName, lastName, fullName }) {
        state.user.firstName = firstName;
        state.user.lastName = lastName;
        state.user.fullName = fullName;
    },
    [SET_CURRENCIES](state, currencies) {
        state.currencies = currencies;
    }
};

const getters = {
    nameInitials: (state) => {
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