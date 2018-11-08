<template>
    <div>
        <div class="modal-body" style="text-align:center">
            Are you sure you want to delete this project ?
        </div>
        <div style="text-align:center">
            <b-button type="button" to="/" class="btn btn-secondary">No</b-button>
            <b-button type="button" v-on:click="deleteProject" class="btn-success">Yes</b-button>
        </div>
    </div>
</template>

<script src="./index.ts"></script>