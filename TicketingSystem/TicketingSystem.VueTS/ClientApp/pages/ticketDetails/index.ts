import Vue from "vue";
import Component from "vue-class-component";
import * as ticketsApi from '../../api/tickets';
import * as messagesApi from '../../api/messages';
import { File } from "../../api";

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

	private get hasMessages(): boolean {
		return this.renderTicket.messages !== null;
	}

	private get userRole(): string {
		return this.$store.getters.sessionInfo.role;
	}

	private async getTicket(): Promise<TicketViewModel> {
		const request: number = this.id;

		const response: ticketsApi.TicketModel = await ticketsApi.tickets.details(request);

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

	private async downloadMessageFile(id: number): Promise<void> {
		const response: File = await messagesApi.messages.downloadFiles(id);

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
}

interface TicketViewModel {
	id: number,
	postTime: Date,
	projectId: number,
	project: string,
	sender: string,
	senderId: string,
	ticketType: TicketType,
	ticketState: TicketState,
	title: string,
	description: string,
	attachedFiles: [],
	messages: MessageModel[]
}

export interface TicketModel {
	id: number,
	postTime: Date,
	projectId: number,
	project: string,
	sender: string,
	senderId: string,
	ticketType: TicketType,
	ticketState: TicketState,
	title: string,
	description: string,
	attachedFiles: [],
	messages: MessageModel[]
}

interface MessageModel {
	id: number,
	postDate: string,
	authorId: string,
	author: string,
	ticketId: number,
	ticket: TicketModel,
	state: MessageState,
	content: string,
	attachedFiles: []
}

enum TicketType {
	BugReport,
	FeatureRequest,
	AssistanceRequest,
	Other
}

enum TicketState {
	Draft,
	New,
	Running,
	Completed
}

enum MessageState {
	Draft,
	Published
}