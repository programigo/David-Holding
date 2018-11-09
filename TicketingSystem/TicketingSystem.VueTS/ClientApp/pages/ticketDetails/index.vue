<template>
    <div style="text-align:center">
        <b-card :title="renderTicket.title" tag="article" style="max-width: 45rem;" class="mb-2">
            <p>Published on {{renderTicket.postTime}} by {{renderTicket.sender}}</p>
            <hr />
            <p class="card-text">
                {{renderTicket.description}}
            </p>
            <b-button v-bind:to="{ name: 'ticket-edit'}" variant="warning">Edit</b-button>
            <b-button v-bind:to="{ name: 'ticket-delete'}" variant="danger">Delete</b-button>
        </b-card>

        <div>
            <b-btn v-b-toggle.collapse1 variant="primary">Messages({{renderTicket.messages.length}})</b-btn>
            <b-collapse id="collapse1" class="mt-2">
                <b-card v-if="renderTicket.messages.length">
                    <div v-for="message in renderTicket.messages" id="message-toggle-div">

                        <b-card tag="article" style="max-width: 40rem;" class="mb-2">
                            <p>Sent on  {{message.postDate}}, {{message.postDate.getHours}} by {{message.author}}</p>
                            <p class="card-text">
                                {{message.content}}
                            </p>
                            <div style="text-align:center">
                                <b-button v-if="message.attachedFiles" v-on:click="downloadMessageFile(message.id)" style="background-color:blue">Download Files</b-button>
                                <b-button v-bind:to="{name: 'message-attach-files'}" style="background-color:blueviolet">Attach Files</b-button>
                            </div>
                        </b-card>

                    </div>

                </b-card>
                <p v-else>No messages to display.</p>
            </b-collapse>
        </div>
    </div>
</template>

<script src="./index.ts"></script>