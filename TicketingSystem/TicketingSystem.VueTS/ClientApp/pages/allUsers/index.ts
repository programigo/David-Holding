﻿import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api/users';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component({

})

export default class AllUsers extends Vue {
    allUsers: UserListingViewModel = {
        users: null,
        roles: null
    };

    public async mounted(): Promise<UserListingViewModel> {
        return this.getAllUsers();
    }

    private async getAllUsers(): Promise<UserListingViewModel> {
        const response: api.UserListingModel = await api.users.getUsers();

        const users: AdminUserListingViewModel[] = response.users;
        const roles: SelectListItem[] = response.roles;

        const result: UserListingViewModel = {
            users: users,
            roles: roles
        }

        this.allUsers.users = result.users;
        this.allUsers.roles = result.roles;

        return result;
    }

    private async remove(id: string): Promise<void> {
        const response: void = await api.users.removeUser(id);

        return response;
    }
}

interface UserListingViewModel {
    users: AdminUserListingViewModel[];
    roles: SelectListItem[];
}

interface AdminUserListingViewModel {
    id: string;
    username: string;
    email: string;
    isApproved: boolean;
}

interface SelectListItem {
    text: string;
    value: string;
}