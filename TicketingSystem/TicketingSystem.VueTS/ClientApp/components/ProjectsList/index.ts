import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api/projects';
import { ProjectModel } from '../../api/projects';

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
    //private async getAllProjects(): Promise<ProjectModel[]> {
    //    const response: api.ProjectModel[] = await api.projects.getProjects();
    //
    //    const projects: ProjectViewModel[] = response;
    //
    //    return projects;
    //}

    //private get allProjects(): 
    projects: Project[] = [];
    
    mounted() {
        fetch('api/projects')
            .then(response => response.json() as Promise<Project[]>)
            .then(data => {
                this.projects = data;
            })
    }
}

//interface ProjectListingViewModel {
//    projects: ProjectModel[];
//    totalProjects: number;
//    totalPages: number;
//    currentPage: number;
//    previousPage: number;
//    nextPage: number;
//}

interface ProjectViewModel {
    id: number;
    name: string;
    description: string;
}