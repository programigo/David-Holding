<template>
	<div>
		<div v-if="hasUsers">
			<h1 class="form-title">User Administration</h1>
			<br />
			<table id="all-users" class="table table-hover table-bordered">
				<thead>
					<tr>
						<th>Username</th>
						<th>Email</th>
						<th></th>
					</tr>
				</thead>
				<tbody v-for="user in allUsers.users">
					<tr>
						<th>{{user.username}}</th>
						<th>{{user.email}}</th>
						<th>
							<div class="add-to-role row">

								<b-form @submit.prevent="addUserToRole(user.id)">

									<select v-model="addToRoleModel.role">
										<option v-for="role in allUsers.roles">{{role.text}}</option>
									</select>

									<b-button type="submit" variant="success">Add To Role</b-button>
								</b-form>

								<div class="user-actions col-md-8">
									<b-button :to="{ path: 'users/changeuserpassword/' + user.id }" variant="success">Change Password</b-button>
									<b-button :to="{ path: 'users/changeuserdata/' + user.id }" variant="warning">Change Data</b-button>
									<b-button v-on:click="remove(user.id)" variant="danger">Remove</b-button>
								</div>
							</div>
						</th>
					</tr>
				</tbody>
			</table>
		</div>
		<p v-else>There are no registered users.</p>
	</div>

</template>

<script src="./index.ts"></script>