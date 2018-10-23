import { ControllerBase } from '../ControllerBase';
import { ProjectListingModel, AddProjectFormModel, ProjectModel } from './types';

export class ProjectsController extends ControllerBase {
    public constructor() {
        super("api/projects");
    }

    public async getProjects(): Promise<ProjectModel[]> {
        const response = await super.ajaxGet<void, ProjectModel[]>("");

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

    public async edit(id: number): Promise<AddProjectFormModel> {
        const response = await super.ajaxPut<number, AddProjectFormModel>(`edit/${id}`);

        return response.data;
    }

    public async delete(id: number): Promise<boolean> {
        const response = await super.ajaxDelete<void, boolean>(`delete/${id}`);

        return response.data;
    }
}