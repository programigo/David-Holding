import Vue from "vue";
import Component from "vue-class-component";
import * as manageApi from '../../api/manage';
import * as api from '../../api';

@Component

export default class ChangePassword extends Vue {
	changePasswordViewModel: ChangePasswordViewModel = {
		oldPassword: null,
		newPassword: null,
		confirmPassword: null
	}

	error: string = null;

	get hasError(): boolean {
		return this.error !== null;
	}

	private async changePassword(): Promise<void> {
		try {
			const request: manageApi.ChangePasswordModel = {
				oldPassword: this.changePasswordViewModel.oldPassword,
				newPassword: this.changePasswordViewModel.newPassword,
				confirmPassword: this.changePasswordViewModel.confirmPassword
			}

			const response: void = await manageApi.manage.changePassword(request);

			this.$router.push('/');

			return response;

		} catch (e) {
			const error = <api.ErrorModel>e.response.data;
			this.error = error.message;
		}
	}
}

interface ChangePasswordViewModel {
	oldPassword: string;
	newPassword: string;
	confirmPassword: string;
}