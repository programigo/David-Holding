import Vue from 'vue';
import Vuex from 'vuex';
import createPersistedState from 'vuex-persistedstate';
import { state } from './state';
import { getters } from './getters';
import { actions } from './actions';
import { mutations } from './mutations';
import { AppState } from './types';

Vue.use(Vuex);

export const APP_STATE_KEY = "APP_STATE";

export const store = new Vuex.Store<AppState>({
	state: state,
	getters: getters,
	actions: actions,
	mutations: mutations,
	plugins: [createPersistedState({
		key: APP_STATE_KEY
	})]
});

window.addEventListener("storage", (ev: StorageEvent) => {
	// TODO: fined better way to trigger state changed
	if (ev.key === APP_STATE_KEY) {
		const state: AppState = JSON.parse(ev.newValue);

		store.replaceState(state);
	}
});
