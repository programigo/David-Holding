import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api';
import * as ticketsApi from '../../api/tickets';

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

    error: string = null;

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

    get hasError(): boolean {
        return this.error !== null;
    }

    private get userRole(): string {
        return this.$store.getters.sessionInfo.role;
    }

    public async mounted(): Promise<SelectListItem[]> {
        return await this.getProjects();
    }

    private async getProjects(): Promise<SelectListItem[]> {
        const response: SelectListItem[] = await ticketsApi.tickets.getProjects();

        this.createTicketViewModel.projects = response;

        return response;
    }

    private async create(): Promise<void> {
        try {
            const request: ticketsApi.SubmitTicketFormModel = {
                title: this.createTicketViewModel.title,
                description: this.createTicketViewModel.description,
                postTime: new Date().getHours() + ':' + new Date().getMinutes() + ':' + new Date().getSeconds() + ' ' + new Date().getUTCDay() + '/' + new Date().getUTCMonth() + '/' + new Date().getUTCFullYear(),
                ticketType: this.createTicketViewModel.ticketType,
                ticketState: this.createTicketViewModel.ticketState,
                projectId: this.createTicketViewModel.projectId,
                projects: this.createTicketViewModel.projects
            }

            const response: void = await ticketsApi.tickets.create(request);

            this.$router.push('/tickets');

            return response;

        } catch (e) {
            const error = <api.ErrorModel>e.response.data;
            this.error = error.message;
        }
    }

    private validateBeforeCreate(): void {
        this.$validator.validateAll(this.createTicketViewModel)
            .then(result => {
                if (!result) {
                } else {
                    this.create();
                }
            });
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