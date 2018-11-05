import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/messages';

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

    messageStates: SelectListItem[] = [
        { text: 'Draft', value: api.MessageState.Draft.toString() },
        { text: 'Published', value: api.MessageState.Published.toString() }
    ];

    public async mounted(): Promise<SelectListItem[]> {
        return await this.getTickets();
    }

    private async getTickets(): Promise<SelectListItem[]> {
        const response: SelectListItem[] = await api.messages.getTickets();

        this.createMessageViewModel.tickets = response;

        return response;
    }

    private async create(): Promise<void> {
        const request: api.AddMessageFormModel = {
            postDate: new Date().getHours() + ':' + new Date().getMinutes() + ':' + new Date().getSeconds() + ' ' + new Date().getUTCDay() + '/' + new Date().getUTCMonth() + '/' + new Date().getUTCFullYear(),
            state: this.createMessageViewModel.state,
            content: this.createMessageViewModel.content,
            ticketId: this.createMessageViewModel.ticketId,
            tickets: this.createMessageViewModel.tickets
        }

        const response: void = await api.messages.create(request);

        this.$router.push('/tickets');

        return response;
    }
}

interface CreateMessageViewModel {
    postDate: string,
    state: api.MessageState,
    content: string,
    ticketId: number,
    tickets: SelectListItem[]
}