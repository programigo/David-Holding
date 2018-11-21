import axios, { AxiosResponse, AxiosRequestConfig } from 'axios';

export abstract class ControllerBase {
	private readonly _baseUrl: string;

	protected constructor(baseUrl: string) {
		this._baseUrl = baseUrl;
	}

	protected async ajaxGet<TRequest, TResponse>(relativeUrl: string, query?: TRequest, responseType?: string): Promise<AxiosResponse<TResponse>> {
		const url: string = `${this._baseUrl}/${relativeUrl}`;
		const config: AxiosRequestConfig = {
			params: query,
			responseType: responseType
		};

		const response: AxiosResponse<TResponse> = await axios.get<TResponse>(url, config);

		return response;
	}

	protected async ajaxPost<TRequest, TResponse>(relativeUrl: string, data?: TRequest): Promise<AxiosResponse<TResponse>> {
		const url: string = `${this._baseUrl}/${relativeUrl}`;
		const config: AxiosRequestConfig = {
			data: data,
		};

		const response: AxiosResponse<TResponse> = await axios.post<TResponse>(url, null, config);

		return response;
	}

	protected async ajaxPostFile<TRequest, TResponse>(relativeUrl: string, data?: TRequest, headers?: any): Promise<AxiosResponse<TResponse>> {
		const url: string = `${this._baseUrl}/${relativeUrl}`;
		const config: AxiosRequestConfig = {
			headers: headers
		};

		const response: AxiosResponse<TResponse> = await axios.post<TResponse>(url, data, config);

		return response;
	}

	protected async ajaxPut<TRequest, TResponse>(relativeUrl: string, data?: TRequest): Promise<AxiosResponse<TResponse>> {
		const url: string = `${this._baseUrl}/${relativeUrl}`;
		const config: AxiosRequestConfig = {
			data: data
		};

		const response: AxiosResponse<TResponse> = await axios.put<TResponse>(url, null, config);

		return response;
	}

	protected async ajaxDelete<TRequest, TResponse>(relativeUrl: string, data?: TRequest): Promise<AxiosResponse<TResponse>> {
		const url: string = `${this._baseUrl}/${relativeUrl}`;
		const config: AxiosRequestConfig = {
			data: data
		};

		const response: AxiosResponse<TResponse> = await axios.delete(url, config);

		return response;
	}
}
