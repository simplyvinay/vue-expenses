import axios from 'axios';
import store from "@/store"
import { ADD_ALERT, TOGGLE_LOADING } from '@/store/_actionTypes'

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
    updateLoader(true);    
    return request;
});

api.interceptors.response.use(function (response) {
    updateLoader(false);
    return response;
}, function (error) {
    updateLoader(false);
    var errormessage = error.response && error.response.data.errors && error.response.data.errors.Error
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

    store.dispatch(`alert/${ADD_ALERT}`, { message: errormessage, color: 'error' }, { root: true });

    return Promise.reject(error);
});

function updateLoader(loading){
    store.dispatch(`loader/${TOGGLE_LOADING}`, { loading: loading }, { root: true });
}

export default api;