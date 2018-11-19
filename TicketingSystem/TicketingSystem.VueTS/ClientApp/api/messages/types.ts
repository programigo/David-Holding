import { SelectListItem } from "../users";

export interface AddMessageFormModel {
	postDate: string,
	state: MessageState,
	content: string,
	ticketId: number,
	tickets: SelectListItem[]
}

export enum MessageState {
	Draft,
	Published
}