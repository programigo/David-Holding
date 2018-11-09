import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api';
import * as actions from '../../store/actions';
import { LoginActionPayload } from '../../store/actions';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component

export default class Login extends Vue {
    loginViewModel: LoginViewModel = {
        username: null,
        password: null
    };

    error: string = null;

    get hasError(): boolean {
        return this.error !== null;
    }

    private async login(): Promise<void> {
        try {
            const request: api.LoginRequest = {

                username: this.loginViewModel.username,
                password: this.loginViewModel.password
            }

            const response: api.LoginResult = await api.account.logIn(request);
            const role: string = await api.account.getUserRole(response.id);

            const payload: LoginActionPayload = {
                sessionInfo: {
                    role: role,
                    userName: response.userName
                }
            }

            await this.$store.dispatch(actions.LOGIN, payload);

            this.$router.push('/');

        } catch (e) {
            const error = <api.ErrorModel>e.response.data;
            this.error = error.message;
        }
        
    }

    private validateBeforeLogin(): void {
        this.$validator.validateAll(this.loginViewModel)
            .then(result => {
                if (!result) {
                } else {
                    this.login();
                }
            });
    }
}

interface LoginViewModel {
    username: string;
    password: string;
};