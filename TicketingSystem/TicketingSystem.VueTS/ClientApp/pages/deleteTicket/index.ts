import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/tickets';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component

export default class DeleteTicket extends Vue {
    private get id(): number {
        return Number(this.$route.params.ticketId);
    }

    private async deleteTicket(): Promise<void> {
        const request: number = this.id;

        const response: void = await api.tickets.delete(request);

        this.$router.push('/tickets');

        return response;
    }
}