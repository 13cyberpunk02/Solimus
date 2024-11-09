import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { AuthService } from "../CommonServices/Authentication/auth.service";
import { Router } from "@angular/router";
import { inject } from "@angular/core";
import { catchError, throwError } from "rxjs";
import { MainSuccessResponse } from "../../Models/Reponse/mainSuccessReponse";
import { RefreshTokenRequest } from "../../Models/Requests/Authentication/refreshToken";
import { ToastService, ToastType } from "../CommonServices/Toast/toast.service";

export const AuthInterceptor: HttpInterceptorFn = (request, next) => {

    const authService = inject(AuthService);
    const router = inject(Router);
    const toast = inject(ToastService);

    const token = authService.getToken();

    if (token) {
        const cloned = request.clone({
            headers: request.headers.set('Authorization', `Bearer ${token}`)
        });

        return next(cloned).pipe(
            catchError((error: HttpErrorResponse) => {
                if(error.status === 401) {
                    const refreshTokenModel = authService.getRefreshTokenModel();
                    if(refreshTokenModel) {
                        authService.refreshAccessToken(refreshTokenModel).subscribe({
                            next: (response) => {
                                const result = response as MainSuccessResponse;
                                const requestModel: RefreshTokenRequest = {
                                    accessToken: result.value.accessToken,
                                    refreshToken: result.value.refreshToken,
                                    email: result.value.email
                                };
                                localStorage.setItem('token', JSON.stringify(requestModel));
                                request.clone({
                                    setHeaders: {
                                        Authorization: `Bearer ${requestModel.accessToken}`
                                    }
                                });
                                location.reload();
                            },
                            error: (response: HttpErrorResponse) => {                                
                                authService.logout();
                                toast.show(response.error.error.message, ToastType.Error);
                                router.navigate(['/']);
                            }
                        });
                    }
                    toast.show('Вы не авторизованы', ToastType.Warning);
                    router.navigate(['/']);
                }
                return throwError( () => error);
            })
        );
    }
    return next(request);
};
