<template>
    <div>
        <div v-if="renderProjects.length">
            <div v-for="project in renderProjects">
                <div class="col-md-3">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center">{{ project.name }}</div>
                        <div class="panel-body" style="text-align: center">{{ project.description }}</div>
                    </div>
                </div>
            </div>
        </div>
        <p v-else><em>Loading...</em></p>
    </div>

</template>

<script src="./index.ts"></script>