import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
import { router } from './router';
Vue.use(VueRouter);

//const routes = [
//    { path: '/', component: require('./pages/home/index.vue') },
//    { path: '/counter', component: require('./components/counter/counter.vue.html') },
//    { path: '/fetchdata', component: require('./components/fetchdata/fetchdata.vue.html') },
//    { path: '/projects', component: require('./pages/projects/index.vue') },
//    { path: '/register', component: require('./components/register/register.vue.html') },
//    { path: '/login', component: require('./pages/login/index.vue') }
//];

new Vue({
    el: '#app-root',
    router: router,
    render: h => h(require('./components/app/app.vue.html'))
});
