import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface Project {
    id: number,
    name: string,
    description: string
}

@Component
export default class ProjectsComponent extends Vue {
    projects: Project[] = [];

    mounted() {
        fetch('api/home')
            .then(response => response.json() as Promise<Project[]>)
            .then(data => {
                this.projects = data;
            })
    }
}