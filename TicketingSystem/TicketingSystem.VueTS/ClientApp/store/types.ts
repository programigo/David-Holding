export interface AppState {
	sessionInfo: SessionInfo,
	isLoggedIn: boolean
};

export interface SessionInfo {
	userName: string;
}
