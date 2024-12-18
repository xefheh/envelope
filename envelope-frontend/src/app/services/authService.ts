import { AuthResponse } from './response/authResponse';
import { useRouter } from 'next/router';

export const login = async (login: string, password: string) => {
	try {
		const answer = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/auth/login`, {
			method: 'POST',
			body: JSON.stringify({
				login: login,
				password: password
			}),
			headers: {
			"Content-Type": "application/json",
		}});

		if(answer.status !== 200) {
			throw new Error('Неудача(');
		}

		const response: AuthResponse = await answer.json()

		localStorage.setItem('name', response.nickname);
		localStorage.setItem('token', response.token);
		localStorage.setItem('userid', response.userId); 
	} catch(e) {
		alert(e);
		throw e;
	}
}

export const register = async (email: string, login: string, password: string) => {
	try {
		const answer = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/auth/register`, {
			method: 'POST',
			body: JSON.stringify({
				email: email,
				nickname: login,
				password: password
			}),
			headers: {
			"Content-Type": "application/json",
		}});

		if(answer.status !== 200) {
			throw new Error('Неудача(');
		}

		const response: AuthResponse = await answer.json()
		localStorage.setItem('name', response.nickname);
		localStorage.setItem('token', response.token);
		localStorage.setItem('userid', response.userId);
	} catch(e) {
		alert(e);
		throw e;
	}
}

export const checkLoggedState = (): boolean => {
	if(localStorage.getItem('name') == null ||
		localStorage.getItem('token') == null ||
		localStorage.getItem('userid') == null) {
				console.log('б')
			return false;
		}
	return true;
}

export const logout = () => {
	localStorage.removeItem('name');
	localStorage.removeItem('token');
	localStorage.removeItem('userid');
}