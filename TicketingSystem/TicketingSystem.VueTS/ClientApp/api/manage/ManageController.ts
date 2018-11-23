import { ControllerBase } from '../ControllerBase';
import { ChangePasswordModel } from './types';

export class ManageController extends ControllerBase {
	public constructor() {
		super("api/manage");
	}

	public async changePassword(model: ChangePasswordModel): Promise<void> {
		await super.ajaxPut<ChangePasswordModel, null>("changepassword", model);
	}
}