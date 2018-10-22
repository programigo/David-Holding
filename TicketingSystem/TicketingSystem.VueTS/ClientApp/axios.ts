import axios from 'axios'
import root from './boot';

import { store } from './store';
import * as actions from './store/actions';

import { router } from './router';

import * as api from './api';

axios.interceptors.response.use(response => {
	return response;
}, error => {
	if (error && error.response) {
		AxiosProcessResponse(error.response);
	} else if (error.message) {
		console.log(error.message);
	} else {
		console.log("AXIOS response interceptor error.");
	}

	return Promise.reject(error);
});

function AxiosProcessResponse(response: any) {
	switch (response.status) {
		case 401: {
			const payload: actions.UnauthorizedActionPayload = {
			};

			store.dispatch(actions.UNAUTHORIZED, payload);

			router.push('/login');

			break;
		}
		case 500: {
			if (!store.getters.isLoggedIn) {
				return;
			}

			const errorResponse = <api.InternalErrorModel>response.data;
			console.error(errorResponse.message);

			break;
		}
		default: {
			break;
		}
	}
}

function getErrorModalTitle(exceptionType: string): string {
	switch (exceptionType) {
		case "InvalidFileException":
			return "Error processing file"
		default:
			return "An unexpected error has occurred";
	}
}

function ConvertArrayBufferToJson(arrBuffer: ArrayBuffer): string {
	var textEncoding = require('text-encoding');
	var TextDecoder = textEncoding.TextDecoder;

	const bufferAsArray = new Uint8Array(arrBuffer);
	const json = new TextDecoder('utf-8').decode(bufferAsArray);

	return json;
}
