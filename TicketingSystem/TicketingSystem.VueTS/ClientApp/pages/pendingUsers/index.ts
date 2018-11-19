import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/users';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component

export default class PendingUsers extends Vue {
	pendingUsers: UserPendingViewModel = {
		users: null
	}

	public async mounted(): Promise<api.UserPendingModel> {
		return await this.getPendingUsers();
	}

	public async updated(): Promise<api.UserPendingModel> {
		return await this.getPendingUsers();
	}

	private get hasPendingUsers(): boolean {
		return this.pendingUsers !== null;
	}

	private async getPendingUsers(): Promise<api.UserPendingModel> {
		const response: api.UserPendingModel = await api.users.pending();

		this.pendingUsers.users = response.users;

		return response;
	}

	private async approveUser(id: string): Promise<void> {
		const result = await api.users.approve(id);

		return result;
	}
}

interface UserPendingViewModel {
	users: AdminUserListingViewModel[];
}

interface AdminUserListingViewModel {
	id: string;
	username: string;
	email: string;
	isApproved: boolean;
}