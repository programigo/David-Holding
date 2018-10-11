export interface LoginRequest {
    username: string;
    password: string;
}

export interface RegisterRequest {
    username: string;
    name: string;
    email: string;
    password: string;
    confirmPassword: string;
}