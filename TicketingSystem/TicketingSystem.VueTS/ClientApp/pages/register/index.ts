import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api';
import * as actions from '../../store/actions';
import { RegisterActionPayload } from '../../store/actions';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component({

})

export default class Register extends Vue {
    registerViewModel: RegisterViewModel = {
        username: null,
        name: null,
        email: null,
        password: null,
        confirmPassword: null
    };

    private async register(): Promise<void> {
        const request: api.RegisterRequest = {
            username: this.registerViewModel.username,
            name: this.registerViewModel.name,
            email: this.registerViewModel.email,
            password: this.registerViewModel.password,
            confirmPassword: this.registerViewModel.confirmPassword
        }

        const response: api.RegisterResult = await api.account.register(request);
        const payload: RegisterActionPayload = {
            sessionInfo: {
                userName: response.userName
            }
        }

        await this.$store.dispatch(actions.REGISTER, payload);
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
};