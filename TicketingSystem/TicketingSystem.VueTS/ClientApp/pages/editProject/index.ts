import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api/projects';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component({

})

export default class EditProject extends Vue {
    editProjectViewModel: EditProjectViewModel = {
        name: null,
        description: null
    };

    public async mounted(): Promise<void> {
        await this.getProjectInfo();
    }

    private async getProjectInfo(): Promise<api.ProjectModel> {
        const request: number = this.id;

        const response: api.ProjectModel = await api.projects.getDetails(request);

        this.editProjectViewModel.name = response.name;
        this.editProjectViewModel.description = response.description;

        return response;
    }

    private get id(): number {
        return Number(this.$route.params.projectId);
    }

    private async editProject(): Promise<void> {
        const request: api.AddProjectFormModel = {
            name: this.editProjectViewModel.name,
            description: this.editProjectViewModel.description
        }

        const response: void = await api.projects.edit(this.id, request);

        this.$router.push('/');

        return response;
    }
}

interface EditProjectViewModel {
    name: string;
    description: string;
}