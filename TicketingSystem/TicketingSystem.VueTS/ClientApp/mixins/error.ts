import Vue from 'vue';
import { store } from '../store';

function getErrorMessage() {
	const message = store.getters.errorMessage;
	return message;
}

Vue.mixin({
	computed: {
		getErrorMessage(): string {
			return getErrorMessage();
		}
	}
});
