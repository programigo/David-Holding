import { GetterTree } from 'vuex';
import { AppState, SessionInfo } from './types';

export interface AppGetters extends AppState {
	isLoggedIn: boolean;
	sessionInfo: SessionInfo;
};

export const getters: GetterTree<AppState, any> = {
	isLoggedIn: state => state.sessionInfo !== null,
	sessionInfo: state => state.sessionInfo
};
