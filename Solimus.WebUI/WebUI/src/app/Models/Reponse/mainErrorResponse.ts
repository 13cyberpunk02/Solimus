export interface MainErrorResponse {
    isFailure: boolean,
    error: {
        code: string,
        message: string
    }
}