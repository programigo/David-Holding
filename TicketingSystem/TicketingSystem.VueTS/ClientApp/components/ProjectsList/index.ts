import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface Project {
    id: number,
    name: string,
    description: string
}

@Component({
    name: 'projects-list',
    components: {
    }
})

export default class ProjectsList extends Vue {
    projects: Project[] = [];

    mounted() {
        fetch('api/projects')
            .then(response => response.json() as Promise<Project[]>)
            .then(data => {
                this.projects = data;
            })
    }
}