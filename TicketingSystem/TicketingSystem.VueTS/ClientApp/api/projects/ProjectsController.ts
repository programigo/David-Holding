import { ControllerBase } from '../ControllerBase';
import { ProjectListingModel, AddProjectFormModel, ProjectModel } from './types';

export class ProjectsController extends ControllerBase {
	public constructor() {
		super("api/projects");
	}

	public async getProjects(page: number): Promise<ProjectListingModel> {
		const response = await super.ajaxGet<number, ProjectListingModel>(`${page}`);

		return response.data;
	}

	public async create(request?: AddProjectFormModel): Promise<void> {
		const response = await super.ajaxPost<AddProjectFormModel, void>("create", request);

		return response.data;
	}

	public async getDetails(id: number): Promise<ProjectModel> {
		const response = await super.ajaxGet<number, ProjectModel>(`details/${id}`);

		return response.data;
	}

	public async edit(id: number, request?: AddProjectFormModel): Promise<void> {
		const response = await super.ajaxPut<AddProjectFormModel, void>(`edit/${id}`, request);

		return response.data;
	}

	public async delete(id: number): Promise<void> {
		const response = await super.ajaxDelete<number, void>(`delete/${id}`);

		return response.data;
	}
}