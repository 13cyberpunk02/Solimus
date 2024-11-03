export interface MainSuccessResponse {
    value: string,
    isFailure: boolean,
    error: {
        code: string,
        message: string
    }
}