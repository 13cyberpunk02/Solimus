export interface ResetPasswordRequest {
    token: string,
    email: string,
    newPassword: string
}