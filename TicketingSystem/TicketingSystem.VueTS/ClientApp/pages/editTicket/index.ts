import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/tickets';

import VeeValidate from 'vee-validate';
import { SelectListItem } from "../../api/users";

Vue.use(VeeValidate);

@Component

export default class EditTicket extends Vue {
	editTicketModel: EditTicketViewModel = {
		title: null,
		description: null,
		ticketType: null,
		ticketState: null
	};

	ticketTypes: SelectListItem[] = [
		{ text: 'Bug Report', value: TicketType.BugReport.toString() },
		{ text: 'Feature Request', value: TicketType.FeatureRequest.toString() },
		{ text: 'Assistance Request', value: TicketType.AssistanceRequest.toString() },
		{ text: 'Other', value: TicketType.Other.toString() }
	];

	ticketStates: SelectListItem[] = [
		{ text: 'Draft', value: TicketState.Draft.toString() },
		{ text: 'New', value: TicketState.New.toString() },
		{ text: 'Running', value: TicketState.Running.toString() },
		{ text: 'Completed', value: TicketState.Completed.toString() }
	];

	public async mounted(): Promise<void> {
		await this.getTicketInfo();
	}

	private get id(): number {
		return Number(this.$route.params.ticketId);
	}

	private async getTicketInfo(): Promise<api.TicketModel> {
		const request: number = this.id;

		const response: api.TicketModel = await api.tickets.details(request);

		this.editTicketModel.title = response.title;
		this.editTicketModel.description = response.description;
		this.editTicketModel.ticketType = response.ticketType;
		this.editTicketModel.ticketState = response.ticketState;

		return response;
	}

	private async editTicket(): Promise<void> {
		const request: api.EditTicketFormModel = {
			title: this.editTicketModel.title,
			description: this.editTicketModel.description,
			ticketType: this.editTicketModel.ticketType,
			ticketState: this.editTicketModel.ticketState
		};

		await api.tickets.edit(this.id, request);

		this.$router.push('/tickets');
	}
}

interface EditTicketViewModel {
	title: string,
	description: string,
	ticketType: TicketType,
	ticketState: TicketState
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