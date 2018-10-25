<template>
    <div>
        <div v-if="renderProjects.length">
            <div v-for="project in renderProjects">
                <div>
                    <b-card :title="project.name" tag="article" style="max-width: 25rem;" class="mb-2">
                        <b-link :to="{ path: 'projects/details/' + project.id }">
                            {{project.name}}
                        </b-link>
                        <hr />
                        <p class="card-text">
                            {{project.description}}
                        </p>
                        <b-button :to="{ path: 'projects/edit/' + project.id }" variant="warning">Edit</b-button>
                        <b-button :to="{ path: 'projects/delete/' + project.id }" variant="danger">Delete</b-button>
                    </b-card>
                </div>
            </div>
        </div>
        <p v-else><em>Loading...</em></p>
    </div>

</template>

<script src="./index.ts"></script>