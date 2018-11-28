import Vue from 'vue';
import VueRouter, { Route } from 'vue-router';
import { store } from './store';
import { SessionInfo } from './store/types';

Vue.use(VueRouter);

import Home from './pages/home';
import Login from './pages/login';
import Register from './pages/register';
import ChangePassword from './pages/changePassword';
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
import Tickets from './pages/tickets';
import CreateTicket from './pages/createTicket';
import EditTicket from './pages/editTicket';
import DeleteTicket from './pages/deleteTicket';
import TicketDetails from './pages/ticketDetails';
import TicketAttachFiles from './pages/ticketAttachFiles';
import CreateMessage from './pages/createMessage';
import MessageAttachFiles from './pages/messageAttachFiles';

const routes = [
	{ name: 'home', path: '/', component: Home },
	{ name: 'login', path: '/login', component: Login },
	{ name: 'register', path: '/register', component: Register },
	{ name: 'change-password', path: '/manage/changepassword', component: ChangePassword },
	{ name: 'projects', path: '/projects', component: Projects, meta: { auth: true } },
	{ name: 'project-create', path: '/projects/create', component: CreateProject, meta: { auth: true, adminAuth: true } },
	{ name: 'project-details', path: '/projects/details/:projectId', component: ProjectDetails, meta: { auth: true } },
	{ name: 'project-edit', path: '/projects/edit/:projectId', component: EditProject, meta: { auth: true, adminAuth: true } },
	{ name: 'project-delete', path: '/projects/delete/:projectId', component: DeleteProject, meta: { auth: true, adminAuth: true } },
	{ name: 'users', path: '/users', component: AllUsers, meta: { auth: true, adminAuth: true } },
	{ name: 'users-addToRole', path: '/users/addtorole', component: AllUsers, meta: { auth: true, adminAuth: true } },
	{ name: 'register-user', path: '/users/register', component: RegisterUser, meta: { auth: true, adminAuth: true } },
	{ name: 'user-changeData', path: '/users/changeuserdata/:userId', component: ChangeUserData, meta: { auth: true, adminAuth: true } },
	{ name: 'user-changePassword', path: '/users/changeuserpassword/:userId', component: ChangeUserPassword, meta: { auth: true, adminAuth: true } },
	{ name: 'pending-users', path: '/users/pending', component: PendingUsers, meta: { auth: true, adminAuth: true } },
	{ name: 'tickets', path: '/tickets', component: Tickets, meta: { auth: true } },
	{ name: 'ticket-create', path: '/tickets/create', component: CreateTicket, meta: { auth: true } },
	{ name: 'ticket-details', path: '/tickets/details/:ticketId', component: TicketDetails, meta: { auth: true } },
	{ name: 'ticket-edit', path: '/tickets/edit/:ticketId', component: EditTicket, meta: { auth: true } },
	{ name: 'ticket-delete', path: '/tickets/delete/:ticketId', component: DeleteTicket, meta: { auth: true } },
	{ name: 'ticket-attach-files', path: '/tickets/attachfiles/:ticketId', component: TicketAttachFiles, meta: { auth: true } },
	{ name: 'message-create', path: '/messages/create', component: CreateMessage, meta: { auth: true } },
	{ name: 'message-attach-files', path: '/messages/attachfiles/:messageId', component: MessageAttachFiles, meta: { auth: true } },
];

export const router = new VueRouter({
	mode: 'history',
	routes: routes
});

router.beforeEach((to: Route, from: Route, next) => {
	const authRequired: boolean = to.matched.some((route) => route.meta.auth);

	let role;
	let session: SessionInfo = store.state.sessionInfo;
	if (session !== null) {
		role = session.role;
	}

	if (to.meta.adminAuth) {
		if (role === 'Administrator') {
			next();
		} else {
			next('/');
		}
	}

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
