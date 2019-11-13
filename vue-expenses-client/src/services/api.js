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
    var error = error.response && error.response.data && error.response.data.errors
        ? error.response.data.errors.Error
        : error;
    store.dispatch(`alert/${SET_ALERT}`, { message: error, color: 'error' }, { root: true });

    if (error.response && error.response.status === 401) {

    }
    store.dispatch(`loader/${SET_LOADING}`, { loading: false }, { root: true });
    return Promise.reject(error);
});

export default api;