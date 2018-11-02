import Vue from "vue";
import Component from "vue-class-component";
import * as api from '../../api/tickets';
import axios from 'axios';

import VeeValidate from 'vee-validate';
import { SelectListItem } from "../../api/users";

Vue.use(VeeValidate);

@Component

export default class TicketAttachFiles extends Vue {
    attachFileModel: FormData = new FormData();

    private get id(): number {
        return Number(this.$route.params.ticketId);
    }

    private onFileSelected(event?: HTMLInputEvent): void {
        var file = event.target.files[0];
        console.log(event.target.files[0]);
    }

    private startUpload(): void {
        axios.post(`/api/tickets/attachfiles/${this.id}`,
            this.attachFileModel,
            {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

        this.$router.push('/tickets');
    }

    private fileChange(fileList: any): void {
        for (let i = 0; i < fileList.length; i++) {
            this.attachFileModel.append("file", fileList[i], fileList[i].name);
        }
    }

    //private async attachFiles(): Promise<any> {
    //    const request: FormData = this.attachFileModel;
    //
    //    const response: any = await api.tickets.attachFiles(this.id, request);
    //
    //    return response;
    //}
}

interface HTMLInputEvent extends Event {
    target: HTMLInputElement & EventTarget;
}

interface AttachFileViewModel {
    files: FormData
}