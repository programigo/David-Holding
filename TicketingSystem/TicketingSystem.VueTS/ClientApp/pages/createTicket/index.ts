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

    public async mounted(): Promise<SelectListItem[]> {
        return this.getProjects();
    }

    private async getProjects(): Promise<SelectListItem[]> {
        const response: SelectListItem[] = await api.tickets.getProjects();

        this.createTicketViewModel.projects = response;

        return response;
    }

    private async create(): Promise<void> {
        const request: api.SubmitTicketFormModel = {
            title: this.createTicketViewModel.title,
            description: this.createTicketViewModel.description,
            postTime: new Date().getHours() + ':' + new Date().getMinutes() + ':' + new Date().getSeconds() + ' ' + new Date().getUTCDay() + '/' + new Date().getUTCMonth() + '/' + new Date().getUTCFullYear(),
            ticketType: this.createTicketViewModel.ticketType,
            ticketState: this.createTicketViewModel.ticketState,
            projectId: this.createTicketViewModel.projectId,
            projects: this.createTicketViewModel.projects
        }

        const response: void = await api.tickets.create(request);

        this.$router.push('/tickets');

        return response;
    }
}

interface CreateTicketViewModel {
    title: string,
    description: string,
    postTime: string,
    ticketType: TicketType,
    ticketState: TicketState,
    projectId: number,
    projects: SelectListItem[]
}