export interface LoginRequest {
    username: string;
    password: string;
}

export interface LoginResult {
    id: string;
    userName: string;
}

export interface RegisterRequest {
    username: string;
    name: string;
    email: string;
    password: string;
    confirmPassword: string;
}

export interface RegisterResult {
    userName: string;
}