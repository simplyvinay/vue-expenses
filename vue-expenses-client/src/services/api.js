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

export default api;