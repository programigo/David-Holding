export interface LoginRequest {
    username: string;
    password: string;
}

export interface LoginResult {
    userId: string;
    userName: string;
}

export interface RegisterRequest {
    username: string;
    name: string;
    email: string;
    password: string;
    confirmPassword: string;
}