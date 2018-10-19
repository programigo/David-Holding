import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component
export default class Register extends Vue {
    registerViewModel: RegisterViewModel = {
        username: null,
        name: null,
        email: null,
        password: null,
        confirmPassword: null
    };

    private async register(): Promise<void> {

    }
}

interface RegisterViewModel {
    username: string;
    name: string;
    email: string;
    password: string;
    confirmPassword: string;
};