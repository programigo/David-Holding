import { ActionTree } from 'vuex';
import { AppState, SessionInfo } from './types';
import * as mutations from './mutations';

export const UNAUTHORIZED = "UNAUTHORIZED";
export const LOGIN = "LOGIN";
export const LOGOUT = "LOGOUT";
export const UPDATE = "UPDATE";

export const actions: ActionTree<AppState, any> = {
	[UNAUTHORIZED](context, payload: UnauthorizedActionPayload) {
		const mutationPayload: mutations.UnauthorizedMutationPayload = {
		};

		context.commit(mutations.UNAUTHORIZED, mutationPayload);
	},
	[LOGIN](context, payload: LoginActionPayload) {
		const mutationPayload: mutations.LoginMutationPayload = {
			sessionInfo: payload.sessionInfo
		};

		context.commit(LOGIN, mutationPayload);
	},
	[LOGOUT](context, payload: LogoutActionPayload) {
		const mutationPayload: mutations.LogoutMutationPayload = {
		};

		context.commit(LOGOUT, mutationPayload);
	},
	[UPDATE](context, payload: UpdateActionPayload) {
		const mutationPayload: mutations.UpdateMutationPayload = {
			sessionInfo: payload.sessionInfo
		};

		context.commit(UPDATE, mutationPayload);
	}
};

export interface UnauthorizedActionPayload {
}

export interface LoginActionPayload {
	sessionInfo: SessionInfo;
}

export interface LogoutActionPayload {
}

export interface UpdateActionPayload {
	sessionInfo: SessionInfo;
}
