import { ControllerBase } from '../ControllerBase';
import { LoginRequest, RegisterRequest } from './types';

export class AccountController extends ControllerBase {
	public constructor() {
		super("api/account");
    }

    public async logIn(): Promise<void> {
        await super.ajaxPost<void, LoginRequest>("login");
    }

    public async logOut(): Promise<void> {
        await super.ajaxPost<void, void>("logout");
    }

    public async register(): Promise<void> {
        await super.ajaxPost<void, RegisterRequest>("register");
    }
}