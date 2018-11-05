import { ControllerBase } from '../ControllerBase';
import { TicketModel, TicketListingModel, SubmitTicketFormModel, EditTicketFormModel, AttachFileModel } from './types';
import { SelectListItem } from '../users';
import { File } from '../types';

export class TicketsController extends ControllerBase {
    public constructor() {
        super("api/tickets");
    }

    public async getTickets(): Promise<TicketListingModel> {
        const response = await super.ajaxGet<void, TicketListingModel>("");

        return response.data;
    }

    public async create(request?: SubmitTicketFormModel): Promise<void> {
        const response = await super.ajaxPost<SubmitTicketFormModel, void>("create", request);

        return response.data;
    }

    public async edit(id: number, request?: EditTicketFormModel): Promise<void> {
        const response = await super.ajaxPost<EditTicketFormModel, void>(`edit/${id}`, request);

        return response.data;
    }

    public async details(id: number): Promise<TicketModel> {
        const response = await super.ajaxGet<number, TicketModel>(`details/${id}`);

        return response.data;
    }

    public async delete(id: number): Promise<void> {
        const response = await super.ajaxDelete<number, void>(`delete/${id}`);

        return response.data;
    }

    public async getProjects(): Promise<SelectListItem[]> {
        const response = await super.ajaxGet<void, SelectListItem[]>("projects");

        return response.data;
    }

    public async downloadFiles(id: number): Promise<File> {
        const response = await super.ajaxGet<void, any>(`downloadattached/${id}`, null, "blob");

        const file: File = {
            fileName: "response",
            blob: new Blob([response.data], { type: 'application/zip' })
        }

        return file;
    }

    //public async attachFiles(id: number): Promise<void> {
    //    const response = await super.ajaxPost<FormData, any>(`attachfiles/${id}`);
    //
    //    return response.data;
    //}
}