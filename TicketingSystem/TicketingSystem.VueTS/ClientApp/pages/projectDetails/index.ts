﻿import Vue from "vue";
import { Component } from 'vue-property-decorator';
import * as api from '../../api/projects';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component

export default class ProjectDetails extends Vue {
    renderProject: ProjectViewModel = {
        id: null,
        name: null,
        description: null
    }

    public async mounted(): Promise<void> {
        await this.getProject();
    }

    private get id(): number {
        return Number(this.$route.params.projectId);
    }

    private async getProject(): Promise<ProjectViewModel> {
        const request: number = this.id;

        const response: api.ProjectModel = await api.projects.getDetails(request);

        const project: ProjectViewModel = {
            id: response.id,
            name: response.name,
            description: response.description
        };

        this.renderProject = project;

        return project;
    }
}

interface ProjectViewModel {
    id: number;
    name: string;
    description: string;
}