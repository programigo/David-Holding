import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import messages from './messages';
import * as api from '../../api';
import * as actions from '../../store/actions';
import { AppState, SessionInfo } from '../../store/types';

@Component({
	name: "the-nav-menu",
	i18n: {
		messages: messages
	}
})
export default class TheNavMenu extends Vue {
	private userName: string = null;

	private get isLoggedIn(): boolean {
		return this.$store.getters.isLoggedIn;
	}

	private get userRole(): string {
		return this.$store.getters.sessionInfo.role;
	}

	public mounted(): void {
		this.populateData();
	}

	public updated(): void {
		this.populateData();
	}

	public async logout(): Promise<void> {
		await api.account.logOut();

		const payload: actions.LogoutActionPayload = {
		};

		await this.$store.dispatch(actions.LOGOUT, payload);

		this.$router.push({
			path: "/login"
		});
	}

	private populateData(): void {
		if (this.isLoggedIn) {
			const state: AppState = this.$store.state;
			const sessionInfo: SessionInfo = state.sessionInfo;

			if (sessionInfo) {
				this.userName = sessionInfo.userName;
			}
		}
	}
}
