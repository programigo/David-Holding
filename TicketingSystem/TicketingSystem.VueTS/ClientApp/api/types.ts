export const ROOT_ID: string = "0";
export const ME: string = "ME";

export interface File {
    fileName: string,
    blob: Blob
}

export interface ErrorModel {
	message: string;
}

export interface InternalErrorModel extends ErrorModel {
	stackTrace: string;
	innerError: InternalErrorModel;
}

export interface BadRequestErrorModel extends ErrorModel {
	errorType: BadRequestErrorType;
}

export enum BadRequestErrorType {
	MODEL_STATE = "ModelState"
}
