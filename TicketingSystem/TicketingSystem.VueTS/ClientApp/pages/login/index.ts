import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api';

@Component({
    
})

export default class Login extends Vue {
    loginViewModel: LoginViewModel = {
        username: null,
        password: null
    };

    private async login(): Promise<void> {
        const request: api.LoginRequest = {

            username: this.loginViewModel.username,
            password: this.loginViewModel.password
        }

        const response: api.LoginRequest = await api.account.logIn();

        
    }
}

interface LoginViewModel {
    username: string;
    password: string;
};