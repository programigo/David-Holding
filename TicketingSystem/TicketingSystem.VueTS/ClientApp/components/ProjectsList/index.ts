import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api/projects';

@Component({
    name: 'projects-list',
    components: {
    }
})

export default class ProjectsList extends Vue {
    renderProjects: ProjectViewModel[] = [];

    public async created(): Promise<void> {
        await this.getAllProjects();
    }

    private get userRole(): string {
        return this.$store.getters.sessionInfo.role;
    }

    private async getAllProjects(): Promise<ProjectViewModel[]> {
        const response: ProjectModel[] = await api.projects.getProjects();

        

        const projects: ProjectViewModel[] = response
            .map(project => {
                return this.createProjectViewModel(project);
            });

        this.renderProjects = projects;

        return projects;
    }

    private createProjectViewModel(project: ProjectModel): ProjectViewModel {
        const projectViewModel: ProjectViewModel = {
            id: project.id,
            name: project.name,
            description: project.description
        }

        return projectViewModel;
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

export interface ProjectModel {
    id: number;
    name: string;
    description: string;
}