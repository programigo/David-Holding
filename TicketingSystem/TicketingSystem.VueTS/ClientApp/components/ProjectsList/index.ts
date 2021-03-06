﻿import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as api from '../../api/projects';

@Component({
	name: 'projects-list'
})

export default class ProjectsList extends Vue {
	renderProjects: ProjectListingViewModel = {
		projects: null,
		totalProjects: null,
		totalPages: null
	};

	currentPage: number = 1;

	public async mounted(): Promise<void> {
		await this.getAllProjects(this.currentPage);
	}

	public async updated(): Promise<void> {
		await this.getAllProjects(this.currentPage);
	}

	private get hasProjects(): boolean {
		return this.renderProjects.projects !== null;
	}

	private async getAllProjects(page: number): Promise<ProjectListingViewModel> {
		const response: ProjectListingViewModel = await api.projects.getProjects(page);

		if (response.projects !== null) {
			const projects: ProjectViewModel[] = response.projects
				.map(project => {
					return this.createProjectViewModel(project);
				});

			this.renderProjects.projects = projects;
			this.renderProjects.totalProjects = response.totalProjects;
			this.renderProjects.totalPages = response.totalPages;
		}

		return this.renderProjects;
	}

	private createProjectViewModel(project: ProjectViewModel): ProjectViewModel {
		const projectViewModel: ProjectViewModel = {
			id: project.id,
			name: project.name,
			description: project.description
		}

		return projectViewModel;
	}
}

interface ProjectListingViewModel {
	projects: ProjectViewModel[];
	totalProjects: number;
	totalPages: number;
}

interface ProjectViewModel {
	id: number;
	name: string;
	description: string;
}