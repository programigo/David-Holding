import Vue from 'vue';
import { Component, Watch, Inject } from 'vue-property-decorator';
import SignInService from '../../../Services';

export default class Login extends Vue {
    loginViewModel: LoginViewModel = {
        username: null,
        password: null
    };

    redirectUrl: string;
    error: string = null;
}

interface LoginViewModel {
    username: string;
    password: string;
};