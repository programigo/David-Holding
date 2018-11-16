<template>
    <div class="form-title">
        <b-card :title="renderProject.name" tag="article" style="max-width: 45rem;" class="mb-2">
            <hr />
            <p class="card-text">
                {{renderProject.description}}
            </p>
            <div v-if="userRole === 'Administrator'">
                <b-button v-bind:to="{name: 'project-edit'}" variant="warning">Edit</b-button>
                <b-button v-bind:to="{name: 'project-delete'}" variant="danger">Delete</b-button>
            </div>
        </b-card>
    </div>
</template>

<script src="./index.ts"></script>