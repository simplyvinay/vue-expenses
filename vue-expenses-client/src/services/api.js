import axios from 'axios';
import { SET_ALERT } from '@/store/_actionTypes'
import store from "@/store"

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

api.interceptors.response.use(function (response) {
    return response;
}, function (error) {
    var error = error.response ? error.response.data.errors.Error : error;
    store.dispatch(`alert/${SET_ALERT}`, { message: error, color: 'error' }, { root: true });

    if (error.response && error.response.status === 401) {
        
    } 
    return Promise.reject(error);
});

export default api;