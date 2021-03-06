﻿import { ControllerBase } from '../ControllerBase';
import { TicketModel, TicketListingModel, SubmitTicketFormModel, EditTicketFormModel } from './types';
import { SelectListItem } from '../users';
import { File } from '../types';

export class TicketsController extends ControllerBase {
	public constructor() {
		super("api/tickets");
	}

	public async getTickets(page: number): Promise<TicketListingModel> {
		const response = await super.ajaxGet<number, TicketListingModel>(`${page}`);

		return response.data;
	}

	public async create(request?: SubmitTicketFormModel): Promise<void> {
		await super.ajaxPost<SubmitTicketFormModel, void>("create", request);
	}

	public async edit(id: number, request?: EditTicketFormModel): Promise<void> {
		await super.ajaxPost<EditTicketFormModel, void>(`edit/${id}`, request);
	}

	public async details(id: number): Promise<TicketModel> {
		const response = await super.ajaxGet<number, TicketModel>(`details/${id}`);

		return response.data;
	}

	public async delete(id: number): Promise<void> {
		await super.ajaxDelete<number, void>(`delete/${id}`);
	}

	public async getProjects(): Promise<SelectListItem[]> {
		const response = await super.ajaxGet<void, SelectListItem[]>("projects");

		return response.data;
	}

	public async attachFiles(id: number, data: FormData): Promise<void> {
		await super.ajaxPostFile<FormData, null>(`attachfiles/${id}`, data, { 'Content-Type': 'multipart/form-data' });
	}

	public async downloadFiles(id: number): Promise<File> {
		const response = await super.ajaxGet<void, any>(`downloadattached/${id}`, null, "blob");

		const file: File = {
			fileName: "response",
			blob: new Blob([response.data], { type: 'application/zip' })
		}

		return file;
	}
}