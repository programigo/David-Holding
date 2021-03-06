﻿import Vue from "vue";
import Component from "vue-class-component";
import * as ticketsApi from '../../api/tickets';
import * as api from '../../api';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component

export default class TicketAttachFiles extends Vue {
	attachFileModel: FormData = new FormData();

	error: string = null;

	get hasError(): boolean {
		return this.error !== null;
	}

	private get id(): number {
		return Number(this.$route.params.ticketId);
	}

	private async startUpload(): Promise<void> {
		try {
			await ticketsApi.tickets.attachFiles(this.id, this.attachFileModel);

			this.$router.push('/tickets');

		} catch (e) {
			const error = <api.ErrorModel>e.response.data;
			this.error = error.message;
		}
	}

	private validateBeforeUpload(): void {
		this.$validator.validateAll(this.attachFileModel)
			.then(result => {
				if (!result) {
				} else {
					this.startUpload();
				}
			});
	}

	private fileChange(fileList: any): void {
		for (let i = 0; i < fileList.length; i++) {
			this.attachFileModel.append("file", fileList[i], fileList[i].name);
		}
	}
}