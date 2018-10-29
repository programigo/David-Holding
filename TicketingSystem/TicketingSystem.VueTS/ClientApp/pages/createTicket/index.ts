import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/tickets';

import VeeValidate from 'vee-validate';
import { TicketType, TicketState } from "../../api/tickets";
import { SelectListItem } from "../../api/users";

Vue.use(VeeValidate);

@Component

export default class CreateTicket extends Vue {
    createTicketViewModel: CreateTicketViewModel = {
        title: null,
        description: null,
        postTime: null,
        ticketType: null,
        ticketState: null,
        projectId: null,
        projects: null
    };
}

interface CreateTicketViewModel {
    title: string,
    description: string,
    postTime: Date,
    ticketType: TicketType,
    ticketState: TicketState,
    projectId: number,
    projects: SelectListItem[]
}