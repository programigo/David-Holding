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
import DeleteProject from './pages/deleteProject';
import ProjectDetails from './pages/projectDetails';
import AllUsers from './pages/allUsers';
import RegisterUser from './pages/registerUser';
import ChangeUserData from './pages/changeUserData';
import ChangeUserPassword from './pages/changeUserPassword';
import PendingUsers from './pages/pendingUsers';

const routes = [
    { name: 'home', path: '/', component: Home, meta: { requiresAuth: false } },
    { name: 'login', path: '/login', component: Login, meta: { requiresAuth: false } },
    { name: 'register', path: '/register', component: Register, meta: { requiresAuth: false } },
    { name: 'projects', path: '/projects', component: Projects, meta: { requiresAuth: true } },
    { name: 'project-create', path: '/projects/create', component: CreateProject, meta: { requiresAuth: true } },
    { name: 'project-details', path: '/projects/details/:projectId', component: ProjectDetails, meta: { requiresAuth: true } },
    { name: 'project-edit', path: '/projects/edit/:projectId', component: EditProject, meta: { requiresAuth: true } },
    { name: 'project-delete', path: '/projects/delete/:projectId', component: DeleteProject, meta: { requiresAuth: true } },
    { name: 'users', path: '/users', component: AllUsers, meta: { requiresAuth: true } },
    { name: 'users-addToRole', path: '/users/addtorole', component: AllUsers, meta: { requiresAuth: true } },
    { name: 'register-user', path: '/users/register', component: RegisterUser, meta: { requiresAuth: true } },
    { name: 'user-changeData', path: '/users/changeuserdata/:userId', component: ChangeUserData, meta: { requiresAuth: true } },
    { name: 'user-changePassword', path: '/users/changeuserpassword/:userId', component: ChangeUserPassword, meta: { requiresAuth: true } },
    { name: 'pending-users', path: '/users/pending', component: PendingUsers, meta: { requiresAuth: true } },
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
