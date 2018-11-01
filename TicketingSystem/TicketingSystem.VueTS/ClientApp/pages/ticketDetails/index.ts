import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/tickets';

@Component

export default class TicketDetails extends Vue {
    renderTicket: TicketViewModel = {
        id: null,
        postTime: null,
        projectId: null,
        project: null,
        sender: null,
        senderId: null,
        ticketType: null,
        ticketState: null,
        title: null,
        description: null,
        attachedFiles: null,
        messages: null
    };

    public async mounted(): Promise<void> {
        await this.getTicket();
    }

    private get id(): number {
        return Number(this.$route.params.ticketId);
    }

    private async getTicket(): Promise<TicketViewModel> {
        const request: number = this.id;

        const response: api.TicketModel = await api.tickets.details(request);

        const ticket: TicketViewModel = {
            id: response.id,
            postTime: response.postTime,
            projectId: response.projectId,
            project: response.project,
            sender: response.sender,
            senderId: response.senderId,
            ticketType: response.ticketType,
            ticketState: response.ticketState,
            title: response.title,
            description: response.description,
            attachedFiles: response.attachedFiles,
            messages: response.messages
        };

        this.renderTicket = ticket;

        return this.renderTicket;
    }
}

interface TicketViewModel {
    id: number,
    postTime: Date,
    projectId: number,
    project: string,
    sender: string,
    senderId: string,
    ticketType: api.TicketType,
    ticketState: api.TicketState,
    title: string,
    description: string,
    attachedFiles: [],
    messages: api.MessageModel[]
}