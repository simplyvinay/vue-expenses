import axios from 'axios';
import store from "@/store"
import { SET_ALERT, SET_LOADING } from '@/store/_actionTypes'

let api = axios.create({
    baseURL: process.env.VUE_APP_BASE_URL,
    headers: authHeader()
});

function authHeader() {
    // return authorization header with jwt token
    let user = JSON.parse(localStorage.getItem('user'));

    if (user && user.token) {
        return { 'Authorization': 'Bearer ' + user.token };
    } else {
        return {};
    }
}

api.interceptors.request.use(function (request) {
    store.dispatch(`loader/${SET_LOADING}`, { loading: true }, { root: true });
    return request;
});

api.interceptors.response.use(function (response) {
    store.dispatch(`loader/${SET_LOADING}`, { loading: false }, { root: true });
    return response;
}, function (error) {
    store.dispatch(`loader/${SET_LOADING}`, { loading: false }, { root: true });
    var errormessage = error.response && error.response.data.errors.Error
        ? error.response.data.errors.Error
        : error.message;
    
    if (error.response && error.response.status === 401) {
    }

    if (error.response && error.response.status === 422) {
        errormessage = '';
        error.response.data.errors.forEach(function (value) {
            errormessage += value.toString() + ' ';
          });
    }

    store.dispatch(`alert/${SET_ALERT}`, { message: errormessage, color: 'error' }, { root: true });

    return Promise.reject(error);
});

export default api;