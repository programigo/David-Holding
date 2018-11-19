import 'bootstrap';
import Vue from 'vue';
import VueI18n from 'vue-i18n';
import App from './App';
import BootstrapVue from 'bootstrap-vue';
import { store } from './store';
import { router } from './router';

import './mixins';
import './axios';

Vue.use(VueI18n);
Vue.use(BootstrapVue);

export default new Vue({
	el: '#app-root',
	store: store,
	router: router,
	render: h => h(App)
});
