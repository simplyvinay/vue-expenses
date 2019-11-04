import axios from 'axios';

export const userService = {
	login,
	logout
};

function login(email, password) {
	return axios({
		method: 'POST',
		url: `${process.env.VUE_APP_APIURL}/login`,
		data: {
			email,
			password
		}
	})
		.then(response => {
			return response.data;
		})
		.then(user => {
			// login successful if there's a jwt token in the response
			if (user.token) {
				// store user details and jwt token in local storage to keep user logged in between page refreshes
				localStorage.setItem('user', JSON.stringify(user));
			}

			return user;
		})
		.catch(({ response	}) => {
			console.log(response);
			if (response.status === 401) {
				// auto logout if 401 response returned from api
				logout();				
			}
			throw new Error(response.data.errors.Error);

		});
}

function logout() {
	// remove user from local storage to log user out
	localStorage.removeItem('user');
}