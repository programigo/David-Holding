import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/users';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component({

})

export default class ChangeUserData extends Vue {
    changeDataViewModel: AdminChangeDataViewModel = {
        username: null,
        name: null,
        email: null
    };

    public async mounted(): Promise<api.AdminChangeDataModel> {
        return this.getUserInfo();
    }

    private get id(): string {
        return this.$route.params.userId;
    }

    private async getUserInfo(): Promise<api.AdminChangeDataModel> {
        const request: string = this.id;

        const response: api.AdminChangeDataModel = await api.users.getUserInfo(request);

        this.changeDataViewModel.username = response.username;
        this.changeDataViewModel.name = response.name;
        this.changeDataViewModel.email = response.email;

        return response;
    }

    private async changeUserData(): Promise<void> {
        const request: api.AdminChangeDataModel = {
            username: this.changeDataViewModel.username,
            name: this.changeDataViewModel.name,
            email: this.changeDataViewModel.email
        }

        const response: void = await api.users.changeUserData(this.id, request);

        this.$router.push('/users');

        return response;
    }
}

interface AdminChangeDataViewModel {
    username: string;
    name: string;
    email: string;
}