import axios from 'axios';
import store from "@/store"
import { ADD_ALERT, TOGGLE_LOADING } from '@/store/_actiontypes'

let api = axios.create({
    baseURL: process.env.VUE_APP_BASE_URL
});

api.interceptors.request.use((request) => {
    //add authorization header with jwt token to each request
    let user = JSON.parse(localStorage.getItem('user'))
    if (user && user.token) {
        request.headers['Authorization'] = `Bearer ${user.token}`
    }
    updateLoaderTo(true);
    return request;
});

api.interceptors.response.use((response) => {
    updateLoaderTo(false);
    return response;
}, (error) => {
    updateLoaderTo(false);
    var errormessage = error.response && error.response.data.errors && error.response.data.errors.Error
        ? error.response.data.errors.Error
        : error.message;

    if (error.response && error.response.status === 401) {
    }

    if (error.response && error.response.status === 422) {
        errormessage = '';
        error.response.data.errors.forEach((value) => {
            errormessage += value.toString() + ' ';
        });
    }

    store.dispatch(`alert/${ADD_ALERT}`, { message: errormessage, color: 'error' }, { root: true });

    return Promise.reject(error);
});

let updateLoaderTo = (loading) => {
    store.dispatch(`loader/${TOGGLE_LOADING}`, { loading: loading }, { root: true });
}

export default api;