import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/tickets';

@Component({
    name: 'tickets-list'
})

export default class TicketsList extends Vue {
    allTickets: TicketListingViewModel = {
        tickets: null,
        totalTickets: null,
        totalPages: null,
        currentPage: null,
        previousPage: null,
        nextPage: null
    };

    public async mounted(): Promise<void> {
        await this.getAllTickets();
    }

    private async getAllTickets(): Promise<TicketListingViewModel> {
        const response: api.TicketListingModel = await api.tickets.getTickets();

        const tickets: TicketViewModel[] = response.tickets.map(ticket => {
            return this.createTicketViewModel(ticket);
        });

        this.allTickets.tickets = tickets;
        this.allTickets.totalTickets = response.totalTickets;
        this.allTickets.totalPages = response.totalPages;
        this.allTickets.currentPage = response.currentPage;
        this.allTickets.previousPage = response.previousPage;
        this.allTickets.nextPage = response.nextPage;

        return this.allTickets;
    }

    private createTicketViewModel(ticket: api.TicketModel): TicketViewModel {
        const ticketViewModel: TicketViewModel = {
            id: ticket.id,
            postTime: ticket.postTime,
            projectId: ticket.projectId,
            project: ticket.project,
            sender: ticket.sender,
            title: ticket.title,
            description: ticket.description,
            attachedFiles: ticket.attachedFiles
        }

        

        return ticketViewModel;
    }
}

interface TicketListingViewModel {
    tickets: TicketViewModel[],
    totalTickets: number,
    totalPages: number,
    currentPage: number,
    previousPage: number,
    nextPage: number
}

interface TicketViewModel {
    id: number,
    postTime: Date,
    projectId: number,
    project: string,
    sender: string,
    title: string,
    description: string,
    attachedFiles: []
}