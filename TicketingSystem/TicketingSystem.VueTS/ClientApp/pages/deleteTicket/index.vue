<template>
    <div>
        <div class="modal-body">
            Are you sure you want to delete this ticket ?
        </div>
        <div class="modal-footer">
            <button type="button" :to="{path: '/tickets'}" class="btn btn-secondary">No</button>
            <button type="button" v-on:click="deleteTicket" class="btn btn-primary">Yes</button>
        </div>
    </div>
</template>

<script src="./index.ts"></script>