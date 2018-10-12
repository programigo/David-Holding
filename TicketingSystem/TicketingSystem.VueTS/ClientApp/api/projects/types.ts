export interface ProjectListingModel {
    projects: ProjectModel[];
    totalProjects: number;
    totalPages: number;
    currentPage: number;
    previousPage: number;
    nextPage: number;
}

export interface ProjectModel {
    id: number;
    name: string;
    description: string;
}

export interface AddProjectFormModel {
    name: string;
    description: string
}