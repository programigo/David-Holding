import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/tickets';

import VeeValidate from 'vee-validate';
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

    ticketTypes: SelectListItem[] = [
        { text: 'Bug Report', value: api.TicketType.BugReport.toString() },
        { text: 'Feature Request', value: api.TicketType.FeatureRequest.toString() },
        { text: 'Assistance Request', value: api.TicketType.AssistanceRequest.toString() },
        { text: 'Other', value: api.TicketType.Other.toString() }
    ];

    ticketStates: SelectListItem[] = [
        { text: 'Draft', value: api.TicketState.Draft.toString() },
        { text: 'New', value: api.TicketState.New.toString() },
        { text: 'Running', value: api.TicketState.Running.toString() },
        { text: 'Completed', value: api.TicketState.Completed.toString() }
    ];

    private get userRole(): string {
        return this.$store.getters.sessionInfo.role;
    }

    public async mounted(): Promise<SelectListItem[]> {
        return await this.getProjects();
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
    ticketType: api.TicketType,
    ticketState: api.TicketState,
    projectId: number,
    projects: SelectListItem[]
}