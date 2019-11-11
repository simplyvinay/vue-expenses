import axios from 'axios';

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
    if (error.response && error.response.status === 401) {
        console.log('intercepted');
    } 
    return Promise.reject(error);
});

export default api;