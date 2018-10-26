<template>
    <div>
        <div v-if="allUsers.users.length">
            <table class="table table-hover table-bordered">
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
                            <div class="row">
                                <b-button :to="{ path: 'users/changeuserpassword/' + user.id }" variant="success">Change Password</b-button>
                                <b-button :to="{ path: 'users/changeuserdata/' + user.id }" variant="warning">Change Data</b-button>
                                <b-button v-on:click="remove(user.id)" variant="danger">Remove</b-button>
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