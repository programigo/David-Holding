<template>
    <div>
        <div class="form-title">
            Are you sure you want to delete this ticket ?
        </div>
        <div style="text-align:center">
            <b-button type="button" to="/tickets" class="btn btn-secondary">No</b-button>
            <b-button type="button" v-on:click="deleteTicket" class="btn btn-success">Yes</b-button>
        </div>
    </div>
</template>

<script src="./index.ts"></script>