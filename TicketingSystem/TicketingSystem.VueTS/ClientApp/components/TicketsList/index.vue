<template>
    <div>
        <div v-if="allTickets.tickets.length">
            <div v-for="ticket in allTickets.tickets">
                <div>
                    <b-card :title="ticket.title" tag="article" style="max-width: 25rem;" class="mb-2">
                        <b-link :to="{ path: 'tickets/details/' + ticket.id }">
                            {{ticket.title}}
                        </b-link>
                        <p>Published on {{ticket.postTime}} by {{ticket.sender}}</p>
                        <p>Project: <b-link :to="{path: 'projects/details/' + ticket.projectId}">{{ticket.project}}</b-link></p>
                        <hr />
                        <p class="card-text">
                            {{ticket.description}}
                        </p>
                        <b-button :to="{ path: 'tickets/attachfiles/' + ticket.id }" style="background-color:blueviolet">Attach Files</b-button>
                        <b-button :to="{ path: 'tickets/edit/' + ticket.id }" variant="warning">Edit</b-button>
                        <b-button :to="{ path: 'tickets/delete/' + ticket.id }" variant="danger">Delete</b-button>
                    </b-card>
                </div>
            </div>
        </div>
        <p v-else><em>Loading...</em></p>
    </div>
</template>

<script src="./index.ts"></script>