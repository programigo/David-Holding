import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api/projects';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component({

})

export default class CreateProject extends Vue {
    addProjectViewModel: AddProjectViewModel = {
        name: null,
        description: null
    };

    private async create(): Promise<void> {
        const request: api.AddProjectFormModel = {
            name: this.addProjectViewModel.name,
            description: this.addProjectViewModel.description
        }

        const response: void = await api.projects.create(request);

        return response;
    }
}

interface AddProjectViewModel {
    name: string;
    description: string;
}