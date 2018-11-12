import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api';
import * as usersApi from '../../api/users';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component

export default class RegisterUser extends Vue {
    registerViewModel: RegisterViewModel = {
        username: null,
        name: null,
        email: null,
        password: null,
        confirmPassword: null,
        isApproved: null
    };

    error: string = null;

    get hasError(): boolean {
        return this.error !== null;
    }

    private async register(): Promise<void> {
        try {
            const request: usersApi.RegisterModel = {
                username: this.registerViewModel.username,
                name: this.registerViewModel.name,
                email: this.registerViewModel.email,
                password: this.registerViewModel.password,
                confirmPassword: this.registerViewModel.confirmPassword,
                isApproved: true
            };

            const response: void = await usersApi.users.registerUser(request);

            this.$router.push('/users');

            return response;

        } catch (e) {
            const error = <api.ErrorModel>e.response.data;
            this.error = error.message;
        }
    }

    private validateBeforeRegister(): void {
        this.$validator.validateAll(this.registerViewModel)
            .then(result => {
                if (!result) {
                } else {
                    this.register();
                }
            });
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