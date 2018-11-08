<template>
    <div>
        <div v-if="userRole === 'Administrator' || userRole === 'Support'">
            <div v-if="allTickets.tickets.length">
                <div v-for="ticket in allTickets.tickets">
                    <div>
                        <b-card style="max-width: 25rem;" class="mb-2">
                            <b-link :to="{ path: 'tickets/details/' + ticket.id }">
                                <h3 style="text-align:center">{{ticket.title}}</h3>
                            </b-link>
                            <p style="text-align:center">Published on {{ticket.postTime}} by {{ticket.sender}}</p>
                            <p style="text-align:center">Project: <b-link :to="{path: 'projects/details/' + ticket.projectId}">{{ticket.project}}</b-link></p>
                            <hr />
                            <p class="card-text">
                                {{ticket.description}}
                            </p>
                            <hr />
                            <div style="text-align:center">
                                <b-button v-if="ticket.attachedFiles" v-on:click="downloadFile(ticket.id)" style="background-color:blue">Download Files</b-button>
                                <b-button v-if="ticket.sender === $store.getters.sessionInfo.userName" :to="{ path: 'tickets/attachfiles/' + ticket.id }" style="background-color:blueviolet">Attach Files</b-button>
                            </div>
                            <br />
                            <div style="text-align:center">
                                <b-button :to="{ path: 'tickets/edit/' + ticket.id }" variant="warning">Edit</b-button>
                                <b-button :to="{ path: 'tickets/delete/' + ticket.id }" variant="danger">Delete</b-button>
                            </div>

                        </b-card>
                    </div>
                </div>
            </div>
            <p v-else><em>Loading...</em></p>
        </div>
        <div v-else>
        <div v-if="allTickets.tickets.filter(t => t.sender === this.$store.getters.sessionInfo.userName).length">
            <div v-for="ticket in allTickets.tickets.filter(t => t.sender === this.$store.getters.sessionInfo.userName)">
                <div>
                    <b-card style="max-width: 25rem;" class="mb-2">
                        <b-link :to="{ path: 'tickets/details/' + ticket.id }">
                            <h3 style="text-align:center">{{ticket.title}}</h3>
                        </b-link>
                        <p style="text-align:center">Published on {{ticket.postTime}} by {{ticket.sender}}</p>
                        <p style="text-align:center">Project: <b-link :to="{path: 'projects/details/' + ticket.projectId}">{{ticket.project}}</b-link></p>
                        <hr />
                        <p class="card-text">
                            {{ticket.description}}
                        </p>
                        <hr />
                        <div style="text-align:center">
                            <b-button v-if="ticket.attachedFiles" v-on:click="downloadFile(ticket.id)" style="background-color:blue">Download Files</b-button>
                            <b-button v-if="ticket.sender === $store.getters.sessionInfo.userName" :to="{ path: 'tickets/attachfiles/' + ticket.id }" style="background-color:blueviolet">Attach Files</b-button>
                        </div>
                        <br />
                        <div style="text-align:center">
                            <b-button :to="{ path: 'tickets/edit/' + ticket.id }" variant="warning">Edit</b-button>
                        </div>

                    </b-card>
                </div>
            </div>
        </div>
        <p v-else><em>Loading...</em></p>
    </div>
    </div>
</template>

<script src="./index.ts"></script>