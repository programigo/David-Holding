export interface AppState {
	sessionInfo: SessionInfo,
	isLoggedIn: boolean
};

export interface SessionInfo {
	userId: string;
	userName: string;
}
