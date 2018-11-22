import { ControllerBase } from '../ControllerBase';
import { AddMessageFormModel } from './types';
import { SelectListItem } from '../users';
import { File } from '../types';

export class MessagesController extends ControllerBase {
	public constructor() {
		super("api/messages");
	}

	public async create(request?: AddMessageFormModel): Promise<void> {
		await super.ajaxPost<AddMessageFormModel, void>("create", request);
	}

	public async getTickets(): Promise<SelectListItem[]> {
		const response = await super.ajaxGet<void, SelectListItem[]>("tickets");

		return response.data;
	}

	public async attachFiles(id: number, data: FormData): Promise<void> {
		await super.ajaxPostFile<FormData, null>(`attachfiles/${id}`, data, { 'Content-Type': 'multipart/form-data' });
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