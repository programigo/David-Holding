<template>
    <div>
        <b-card :title="renderProject.name" tag="article" style="max-width: 45rem;" class="mb-2">
            <hr />
            <p class="card-text">
                {{renderProject.description}}
            </p>
            <b-button to="projects/edit/:id" variant="warning">Edit</b-button>
            <b-button href="#" variant="danger">Delete</b-button>
        </b-card>
    </div>
</template>

<script src="./index.ts"></script>