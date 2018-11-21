import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/tickets';
import { File } from "../../api";

@Component({
	name: 'tickets-list'
})

export default class TicketsList extends Vue {
	allTickets: TicketListingViewModel = {
		tickets: null,
		totalTickets: null,
		totalPages: null,
	};

	currentPage: number = 1;

	public async created(): Promise<void> {
		await this.getAllTickets(this.currentPage);
	}

	public async updated(): Promise<void> {
		await this.getAllTickets(this.currentPage);
	}

	private get hasTickets(): boolean {
		return this.allTickets.tickets !== null;
	}

	private get userRole(): string {
		return this.$store.getters.sessionInfo.role;
	}

	private async getAllTickets(page: number): Promise<TicketListingViewModel> {
		const response: api.TicketListingModel = await api.tickets.getTickets(page);

		const tickets: TicketViewModel[] = response.tickets.map(ticket => {
			return this.createTicketViewModel(ticket);
		});

		this.allTickets.tickets = tickets;
		this.allTickets.totalTickets = response.totalTickets;
		this.allTickets.totalPages = response.totalPages;

		return this.allTickets;
	}

	private async downloadFile(id: number): Promise<void> {

		const response: File = await api.tickets.downloadFiles(id);

		this.download(response.blob, response.fileName);
	}

	private download(blob: Blob, fileName: string) {
		if (navigator.appVersion.toString().indexOf('.NET') > 0)
			window.navigator.msSaveBlob(blob, fileName);
		else {
			const url = window.URL.createObjectURL(blob);
			const link = document.createElement('a');
			link.href = url;
			link.download = fileName;
			document.body.appendChild(link);
			link.click();
		}
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