import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
import VueI18n from 'vue-i18n';
import App from './App';
import BootstrapVue from 'bootstrap-vue';
import { store } from './store';
import { router } from './router';
import { messages } from './language';

Vue.use(VueI18n);
Vue.use(BootstrapVue);

const i18n = new VueI18n({
    locale: store.getters.culture,
    fallbackLocale: "en-US",
    messages,
});

new Vue({
    el: '#app-root',
    store: store,
    router: router,
    render: h => h(App),
    i18n
});
