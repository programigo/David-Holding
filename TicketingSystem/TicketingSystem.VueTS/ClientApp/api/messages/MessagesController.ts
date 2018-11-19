import { ControllerBase } from '../ControllerBase';
import { AddMessageFormModel } from './types';
import { SelectListItem } from '../users';
import { File } from '../types';

export class MessagesController extends ControllerBase {
	public constructor() {
		super("api/messages");
	}

	public async create(request?: AddMessageFormModel): Promise<void> {
		const response = await super.ajaxPost<AddMessageFormModel, void>("create", request);

		return response.data;
	}

	public async getTickets(): Promise<SelectListItem[]> {
		const response = await super.ajaxGet<void, SelectListItem[]>("tickets");

		return response.data;
	}

	public async downloadFiles(id: number): Promise<File> {
		const response = await super.ajaxGet<void, any>(`downloadattached/${id}`, null, "blob");

		const file: File = {
			fileName: "message-response",
			blob: new Blob([response.data], { type: 'application/zip' })
		}

		return file;
	}
}