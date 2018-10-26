import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/users';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component({

})

export default class RegisterUser extends Vue {
    registerViewModel: RegisterViewModel = {
        username: null,
        name: null,
        email: null,
        password: null,
        confirmPassword: null,
        isApproved: null
    };

    private async register(): Promise<void> {
        const request: api.RegisterModel = {
            username: this.registerViewModel.username,
            name: this.registerViewModel.name,
            email: this.registerViewModel.email,
            password: this.registerViewModel.password,
            confirmPassword: this.registerViewModel.confirmPassword,
            isApproved: true
        };

        const response: void = await api.users.registerUser(request);

        this.$router.push('/users');

        return response;
    }
}

interface RegisterViewModel {
    username: string;
    name: string;
    email: string;
    password: string;
    confirmPassword: string;
    isApproved: boolean;
}