import { ControllerBase } from '../ControllerBase';
import { ProjectListingModel } from './types';

export class ProjectsController extends ControllerBase {
    public constructor() {
        super("api/projects");
    }

    public async getProjects(): Promise<ProjectListingModel> {
        const response = await super.ajaxGet<null, ProjectListingModel>("");

        return response.data;
    }
}