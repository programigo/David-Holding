import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api';

@Component({
    name: 'tickets-list'
})

export default class TicketsList extends Vue {

}

interface TicketListingViewModel {
    tickets: TicketViewModel[],

}

interface TicketViewModel {
    id: number,
    postTime: Date,
    project: string,
    sender: string,
    title: string,
    description: string,

}