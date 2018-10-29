import { ControllerBase } from '../ControllerBase';
import { TicketModel, TicketListingModel, SubmitTicketFormModel } from './types';

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

    public async edit(id: number, request?: SubmitTicketFormModel): Promise<void> {
        const response = await super.ajaxPost<SubmitTicketFormModel, void>(`edit/${id}`, request);

        return response.data;
    }

    public async details(id: number): Promise<TicketModel> {
        const response = await super.ajaxGet<number, TicketModel>(`details/${id}`);

        return response.data;
    }

    public async delete(id: number): Promise<void> {
        const response = await super.ajaxPost<number, void>(`delete/${id}`);

        return response.data;
    }
}