import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api';
import * as projectsApi from '../../api/projects';

import VeeValidate from 'vee-validate';

Vue.use(VeeValidate);

@Component

export default class CreateProject extends Vue {
	addProjectViewModel: AddProjectViewModel = {
		name: null,
		description: null
	};

	error: string = null;

	get hasError(): boolean {
		return this.error !== null;
	}

	private async create(): Promise<void> {
		try {
			const request: projectsApi.AddProjectFormModel = {
				name: this.addProjectViewModel.name,
				description: this.addProjectViewModel.description
			}

			const response: void = await projectsApi.projects.create(request);

			this.$router.push('/');

			return response;

		} catch (e) {
			const error = <api.ErrorModel>e.response.data;
			this.error = error.message;
		}
	}

	private validateBeforeCreate(): void {
		this.$validator.validateAll(this.addProjectViewModel)
			.then(result => {
				if (!result) {
				} else {
					this.create();
				}
			});
	}
}

interface AddProjectViewModel {
	name: string;
	description: string;
}