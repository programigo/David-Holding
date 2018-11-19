import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/users';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component

export default class ChangeUserPassword extends Vue {
	changePasswordViewModel: AdminUserChangePasswordViewModel = {
		newPassword: null
	}

	private get id(): string {
		return this.$route.params.userId;
	}

	private async changeUserPassword(): Promise<void> {
		const request: api.AdminUserChangePasswordModel = {
			newPassword: this.changePasswordViewModel.newPassword
		}

		const response: void = await api.users.changeUserPassword(this.id, request);

		this.$router.push('/users');

		return response;
	}
}

interface AdminUserChangePasswordViewModel {
	newPassword: string;
}