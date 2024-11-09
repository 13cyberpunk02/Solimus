export interface MainSuccessResponse {
    value: any,
    isFailure: boolean,
    error: {
        code: string,
        message: string
    }
}