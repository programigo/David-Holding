import { ControllerBase } from '../ControllerBase';
import { UserListingModel, UserPendingModel, AddUserToRoleFormModel, RegisterModel, AdminChangeDataModel, AdminUserChangePasswordModel } from './types';

export class UsersController extends ControllerBase {
    public constructor() {
        super("api/users");
    }

    public async getUsers(): Promise<UserListingModel> {
        const response = await super.ajaxGet<null, UserListingModel>("");

        return response.data;
    }

    public async pending(): Promise<UserPendingModel> {
        const response = await super.ajaxGet<null, UserPendingModel>("pending");

        return response.data;
    }

    public async addToRole(model: AddUserToRoleFormModel): Promise<void> {
         await super.ajaxPost<AddUserToRoleFormModel, void>("addtorole", model);
    }

    public async approve(id: string): Promise<void> {
        await super.ajaxPost<void, void>(`approve/${id}`);
    }

    public async registerUser(model: RegisterModel): Promise<void> {
        await super.ajaxPost<RegisterModel, void>("register", model);
    }

    public async removeUser(id: string): Promise<void> {
        await super.ajaxPost<void, void>(`remove/${id}`);
    }

    public async changeUserData(id: string, model: AdminChangeDataModel): Promise<void> {
        await super.ajaxPut<AdminChangeDataModel, void>(`changeuserdata/${id}`, model);
    }

    public async changeUserPassword(id: string, model: AdminUserChangePasswordModel): Promise<void> {
        await super.ajaxPut<AdminUserChangePasswordModel, void>(`changeuserpassword/${id}`, model);
    }
}