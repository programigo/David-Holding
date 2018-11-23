import { MutationTree } from 'vuex';
import { AppState, SessionInfo } from './types';

export const UNAUTHORIZED = "UNAUTHORIZED";
export const LOGIN = "LOGIN";
export const LOGOUT = "LOGOUT";
export const REGISTER = "REGISTER";
export const UPDATE = "UPDATE";

export const mutations: MutationTree<AppState> = {
	[UNAUTHORIZED](state: AppState, payload: UnauthorizedMutationPayload): AppState {
		state.sessionInfo = null;
		state.isLoggedIn = false;
		return state;
	},
	[LOGIN](state: AppState, payload: LoginMutationPayload) {
		state.sessionInfo = payload.sessionInfo;
		state.isLoggedIn = true;
		return state;
	},
	[LOGOUT](state: AppState) {
		state.sessionInfo = null;
		state.isLoggedIn = false;
		return state;
	},
	[REGISTER](state: AppState, payload: RegisterMutationPayload) {
		state.sessionInfo = payload.sessionInfo;
		return state;
	},
	[UPDATE](state: AppState, payload: UpdateMutationPayload) {
		state.sessionInfo = null;
		state.sessionInfo = payload.sessionInfo;
		return state;
	}
};

export interface UnauthorizedMutationPayload {
}

export interface LoginMutationPayload {
	sessionInfo: SessionInfo;
}

export interface RegisterMutationPayload {
	sessionInfo: SessionInfo;
}

export interface UpdateMutationPayload {
	sessionInfo: SessionInfo;
}

export interface LogoutMutationPayload {

}

export interface UpdateLocaleMutationPayload {
	culture: string;
}
