<template>
    <div>
        <b-card :title="renderTicket.title" tag="article" style="max-width: 45rem;" class="mb-2">
            <p>Published on {{renderTicket.postTime}} by {{renderTicket.sender}}</p>
            <hr />
            <p class="card-text">
                {{renderTicket.description}}
            </p>
            <b-button :to="'tickets/edit/' + renderTicket.id" variant="warning">Edit</b-button>
            <b-button :to="'tickets/delete/' + renderTicket.id" variant="danger">Delete</b-button>
        </b-card>
    </div>
</template>

<script src="./index.ts"></script>