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

    private ticketTypes: Map<api.TicketType, string> = new Map<api.TicketType, string>([
        [api.TicketType.AssistanceRequest, 'Assistance Request'],
        [api.TicketType.BugReport, 'Bug Report'],
        [api.TicketType.FeatureRequest, 'Feature Request'],
        [api.TicketType.Other, 'Other']
    ]);

    private ticketStates: Map<api.TicketState, string> = new Map<api.TicketState, string>([
        [api.TicketState.Completed, 'Completed'],
        [api.TicketState.Draft, 'Draft'],
        [api.TicketState.New, 'New'],
        [api.TicketState.Running, 'Running']
    ]);

    public async mounted(): Promise<SelectListItem[]> {
        return this.getProjects();
    }

    private onFileSelected(event?: HTMLInputEvent): void{
        var file = event.target.files[0];
        console.log(event.target.files[0]);
    }

    private async getProjects(): Promise<SelectListItem[]> {
        const response: SelectListItem[] = await api.tickets.getProjects();

        this.createTicketViewModel.projects = response;

        //this.ticketTypes.forEach(e => console.log(e));

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

interface HTMLInputEvent extends Event {
    target: HTMLInputElement & EventTarget;
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