import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/projects';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component

export default class DeleteProject extends Vue {
    private get id(): number {
        return Number(this.$route.params.projectId);
    }

    private async deleteProject(): Promise<void> {
        const request: number = this.id;

        const response: void = await api.projects.delete(request);

        this.$router.push('/projects');

        return response;
    }
}