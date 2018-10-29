import { SelectListItem } from "../users";

export interface TicketListingModel {
    tickets: TicketModel[],
    totalTickets: number,
    totalPages: number,
    currentPage: number,
    previousPage: number,
    nextPage: number
}

export interface TicketModel {
    id: number,
    postTime: Date,
    projectId: number,
    project: string,
    sender: string,
    senderId: string,
    ticketType: TicketType,
    ticketState: TicketState,
    title: string,
    description: string,
    attachedFiles: [],
    messages: MessageModel[]
}

export interface MessageModel {
    id: number,
    postDate: Date,
    authorId: string,
    author: string,
    ticketId: number,
    ticket: TicketModel,
    state: MessageState,
    content: string,
    attachedFiles: []
}

export interface SubmitTicketFormModel {
    id: number,
    title: string,
    description: string,
    postTime: Date,
    ticketType: TicketType,
    ticketState: TicketState,
    projectId: number,
    projects: SelectListItem[]
}

export enum TicketType {
    BugReport,
    FeatureRequest,
    AssistanceRequest,
    Other
}

export enum TicketState {
    Draft,
    New,
    Running,
    Completed
}

export enum MessageState {
    Draft,
    Published
}