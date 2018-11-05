<template>
    <div style="text-align: center">
        <b-card :title="renderProject.name" tag="article" style="max-width: 45rem;" class="mb-2">
            <hr />
            <p class="card-text">
                {{renderProject.description}}
            </p>
            <b-button :to="'projects/edit/' + renderProject.id" variant="warning">Edit</b-button>
            <b-button :to="'projects/delete/' + renderProject.id" variant="danger">Delete</b-button>
        </b-card>
    </div>
</template>

<script src="./index.ts"></script>