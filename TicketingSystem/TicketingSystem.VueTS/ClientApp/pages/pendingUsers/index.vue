<template>
    <div>
        <div v-if="pendingUsers.users.length">
            <table class="table table-hover table-bordered">
                <thead>
                    <tr>
                        <th>Username</th>
                        <th>Email</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody v-for="user in pendingUsers.users">
                    <tr>
                        <th>{{user.username}}</th>
                        <th>{{user.email}}</th>
                        <th>
                            <div class="row">
                                <b-button  v-on:click="approveUser(user.id)" variant="success">Approve</b-button>
                            </div>
                        </th>
                    </tr>
                </tbody>
            </table>
        </div>
        <p v-else>There are no pending requests.</p>
        </div>
</template>

<script src="./index.ts"></script>