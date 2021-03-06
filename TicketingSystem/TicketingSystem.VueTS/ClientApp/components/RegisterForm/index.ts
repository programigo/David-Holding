﻿import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component({
	name: 'register-form'
})

export default class RegisterForm extends Vue {
	registerViewModel: RegisterViewModel = {
		username: null,
		name: null,
		email: null,
		password: null,
		confirmPassword: null
	};

	error: string = null;

	get hasError(): boolean {
		return this.error !== null;
	}

	private async register(): Promise<void> {
		try {
			const request: api.RegisterRequest = {
				username: this.registerViewModel.username,
				name: this.registerViewModel.name,
				email: this.registerViewModel.email,
				password: this.registerViewModel.password,
				confirmPassword: this.registerViewModel.confirmPassword
			}

			await api.account.register(request);

			this.$router.push('/');

		} catch (e) {
			const error = <api.ErrorModel>e.response.data;
			this.error = error.message;
		}

	}

	private validateBeforeRegister(): void {
		this.$validator.validateAll(this.registerViewModel)
			.then(result => {
				if (!result) {
				} else {
					this.register();
				}
			});
	}
}

interface RegisterViewModel {
	username: string;
	name: string;
	email: string;
	password: string;
	confirmPassword: string;
};