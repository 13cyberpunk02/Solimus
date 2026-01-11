export interface LoginRequest {
    email: string;
    password: string;
}

export interface LoginResponse {
    accessToken: string;
    refreshToken: string;
}

export interface RefreshTokenRequest {
    accessToken: string;
    refreshToken: string;
}

export interface AuthTokens {
    accessToken: string;
    refreshToken: string;
}

export interface RegistrationRequest {
    email: string;
    userName: string;
    firstName: string;
    lastName: string;
    password: string;
    confirmPassword: string;
}

export interface JwtPayload {
    sub: string;          
    email: string;
    name?: string;
    role?: string | string[];
    exp: number;
}