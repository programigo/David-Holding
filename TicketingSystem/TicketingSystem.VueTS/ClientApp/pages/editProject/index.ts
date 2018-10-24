import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api/projects';

import VeeValidate from 'vee-validate';
import { AddProjectFormModel } from '../../api/projects';

Vue.use(VeeValidate);

@Component({

})

export default class EditProject extends Vue {
    editProjectViewModel: EditProjectViewModel = {
        name: null,
        description: null
    };

    private get id(): number {
        return Number(this.$route.params.projectId);
    }

    private async edit(): Promise<AddProjectFormModel> {
        const request: number = this.id;

        const response: api.AddProjectFormModel = await api.projects.edit(request);

        this.editProjectViewModel.name = response.name;
        this.editProjectViewModel.description = response.description;

        return response;
    }
}

interface EditProjectViewModel {
    name: string;
    description: string;
}