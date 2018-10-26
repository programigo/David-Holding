export interface UserListingModel {
    users: AdminUserListingModel[];
    roles: SelectListItem[];
}

export interface UserPendingModel {
    users: AdminUserListingModel[];
}

export interface AdminUserListingModel {
    id: string;
    username: string;
    email: string;
    isApproved: boolean;
}

export interface SelectListItem {
    text: string;
    value: string;
}

export interface AddUserToRoleFormModel {
    userId: string;
    role: string;
}

export interface RegisterModel {
    username: string;
    name: string;
    email: string;
    password: string;
    confirmPassword: string;
    isApproved: boolean;
}

export interface AdminChangeDataModel {
    username: string;
    name: string;
    email: string;
}

export interface AdminUserChangePasswordModel {
    newPassword: string;
}