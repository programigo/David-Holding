import Vue from 'vue';
import VueRouter, { Route } from 'vue-router';
import { store } from './store';

Vue.use(VueRouter);

import Home from './pages/home';
import Login from './pages/login';
import Register from './pages/register';
import Projects from './pages/projects';
import CreateProject from './pages/createProject';
import EditProject from './pages/editProject';
import ProjectDetails from './pages/projectDetails';

const routes = [
    { name: 'home', path: '/', component: Home, meta: { requiresAuth: false } },
    { name: 'login', path: '/login', component: Login, meta: { requiresAuth: false } },
    { name: 'register', path: '/register', component: Register, meta: { requiresAuth: false } },
    { name: 'projects', path: '/projects', component: Projects, meta: { requiresAuth: true } },
    { name: 'project-create', path: '/projects/create', component: CreateProject, meta: { requiresAuth: true } },
    { name: 'project-details', path: '/projects/details/:projectId', component: ProjectDetails, meta: { requiresAuth: true } },
    { name: 'project-edit', path: '/projects/edit/:projectId', component: EditProject, meta: { requiresAuth: true } },
];

export const router = new VueRouter({
    mode: 'history',
    routes: routes
});

router.beforeEach((to: Route, from: Route, next) => {
    const authRequired: boolean = to.matched.some((route) => route.meta.auth);

    if (store.state.sessionInfo) {
        if (to.name === "login") {
            next({
                path: "home"
            });
        } else {
            next();
        }
    } else if (authRequired) {
        next({
            path: "login",
            query: {
                redirectUrl: to.path
            }
        });
    } else {
        next();
    }
});
