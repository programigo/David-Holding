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
		await super.ajaxPost<AddProjectFormModel, void>("create", request);
	}

	public async getDetails(id: number): Promise<ProjectModel> {
		const response = await super.ajaxGet<number, ProjectModel>(`details/${id}`);

		return response.data;
	}

	public async edit(id: number, request?: AddProjectFormModel): Promise<void> {
		await super.ajaxPut<AddProjectFormModel, void>(`edit/${id}`, request);
	}

	public async delete(id: number): Promise<void> {
		await super.ajaxDelete<number, void>(`delete/${id}`);
	}
}