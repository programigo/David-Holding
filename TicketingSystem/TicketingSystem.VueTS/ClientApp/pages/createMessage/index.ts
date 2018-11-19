import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api';
import * as messagesApi from '../../api/messages';

import VeeValidate from 'vee-validate';
import { SelectListItem } from "../../api/users";

Vue.use(VeeValidate);

@Component

export default class CreateMessage extends Vue {
	createMessageViewModel: CreateMessageViewModel = {
		postDate: null,
		state: null,
		content: null,
		ticketId: null,
		tickets: null
	};

	error: string = null;

	messageStates: SelectListItem[] = [
		{ text: 'Draft', value: MessageState.Draft.toString() },
		{ text: 'Published', value: MessageState.Published.toString() }
	];

	get hasError(): boolean {
		return this.error !== null;
	}

	private get userRole(): string {
		return this.$store.getters.sessionInfo.role;
	}

	public async mounted(): Promise<SelectListItem[]> {
		return await this.getTickets();
	}

	private async getTickets(): Promise<SelectListItem[]> {
		const response: SelectListItem[] = await messagesApi.messages.getTickets();

		this.createMessageViewModel.tickets = response;

		return response;
	}

	private async create(): Promise<void> {
		try {
			const request: messagesApi.AddMessageFormModel = {
				postDate: new Date().getHours() + ':' + new Date().getMinutes() + ':' + new Date().getSeconds() + ' ' + new Date().getUTCDay() + '/' + new Date().getUTCMonth() + '/' + new Date().getUTCFullYear(),
				state: this.createMessageViewModel.state,
				content: this.createMessageViewModel.content,
				ticketId: this.createMessageViewModel.ticketId,
				tickets: this.createMessageViewModel.tickets
			}

			const response: void = await messagesApi.messages.create(request);

			this.$router.push('/tickets');

			return response;

		} catch (e) {
			const error = <api.ErrorModel>e.response.data;
			this.error = error.message;
		}

	}

	private validateBeforeCreate(): void {
		this.$validator.validateAll(this.createMessageViewModel)
			.then(result => {
				if (!result) {
				} else {
					this.create();
				}
			});
	}
}

interface CreateMessageViewModel {
	postDate: string,
	state: MessageState,
	content: string,
	ticketId: number,
	tickets: SelectListItem[]
}

enum MessageState {
	Draft,
	Published
}