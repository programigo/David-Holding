import { ControllerBase } from '../ControllerBase';
import { LoginRequest, LoginResult, RegisterRequest } from './types';

export class AccountController extends ControllerBase {
	public constructor() {
		super("api/account");
	}

	public async logIn(request?: LoginRequest): Promise<LoginResult> {
		const result = await super.ajaxPost<LoginRequest, LoginResult>("login", request);

		return result.data;
	}

	public async isLoggedOn(): Promise<void> {
		await super.ajaxGet<void, void>("isLoggedOn");
	}

	public async logOut(): Promise<void> {
		await super.ajaxPost<void, void>("logout");
	}

	public async register(request?: RegisterRequest): Promise<void> {
		await super.ajaxPost<RegisterRequest, void>("register", request);
	}

	public async getUserRole(id: string): Promise<string> {
		const result = await super.ajaxGet<string, string>(`getrole/${id}`);

		return result.data;
	}
}