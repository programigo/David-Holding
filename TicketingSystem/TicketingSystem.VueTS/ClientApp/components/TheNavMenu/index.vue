<template>
	<b-navbar class="navbar navbar-expand-md navbar-static-top navbar-dark bg-dark">
		<b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
		<b-navbar-brand to="/" :exact="true">Ticketing System</b-navbar-brand>
		<b-collapse is-nav id="nav_collapse">
			<b-navbar-nav v-if="isLoggedIn">
				<b-nav-item-dropdown v-if="userRole === 'Administrator'" text="Users">
					<b-dropdown-item to="/users">User Administration</b-dropdown-item>
					<b-dropdown-item to="/users/pending">Pending Requests</b-dropdown-item>
					<b-dropdown-item to="/users/register">Register New User</b-dropdown-item>
				</b-nav-item-dropdown>
				<b-nav-item v-if="userRole === 'Administrator'" to="/projects/create">Create Project</b-nav-item>
				<b-nav-item v-if="userRole === 'Administrator' || userRole === 'Support'" to="/tickets">All Tickets</b-nav-item>
				<b-nav-item v-else to="/tickets">My Tickets</b-nav-item>
				<b-nav-item to="/tickets/create">Create Ticket</b-nav-item>

				<b-nav-item to="/messages/create">Send Message</b-nav-item>
			</b-navbar-nav>
			<b-navbar-nav class="ml-auto">
				<div v-if="!isLoggedIn">
					<b-nav-item to="/login">
						{{$t('login')}}
					</b-nav-item>
				</div>
				<div v-else class="form-inline">
					<b-nav-text>
						{{$t('hello')}}, <em>{{userName}} !</em>
					</b-nav-text>
					<b-nav-item @click="logout">

						{{$t('logout')}}
					</b-nav-item>
				</div>
			</b-navbar-nav>
		</b-collapse>
	</b-navbar>
</template>

<script src="./index.ts"></script>
