import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface Project {
    id: number,
    name: string,
    description: string
}

@Component
export default class HomeComponent extends Vue {
    projects: Project[] = [];

    mounted() {
        fetch('api/Home/Index')
            .then(response => response.json() as Promise<Project[]>)
            .then(data => {
                this.projects = data;
            });
    }
}