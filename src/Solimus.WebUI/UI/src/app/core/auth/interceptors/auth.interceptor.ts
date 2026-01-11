import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, filter, take, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { environment } from '../../environments/environment';

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const authService = inject(AuthService);

  if (isAuthEndpoint(req.url)) {
    return next(req);
  }

  const accessToken = authService.getAccessToken();

  if (accessToken) {
    req = addTokenToRequest(req, accessToken);
  }

  return next(req);
};

export const refreshTokenInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const authService = inject(AuthService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      // Обрабатываем только 401 ошибки
      if (error.status !== 401) {
        return throwError(() => error);
      }

      // Пропускаем auth endpoints
      if (isAuthEndpoint(req.url)) {
        authService.forceLogout();
        return throwError(() => error);
      }

      if (authService.isRefreshing) {
        return authService.refreshToken$.pipe(
          filter(token => token !== null),
          take(1),
          switchMap(token => {
            return next(addTokenToRequest(req, token!));
          })
        );
      }

      return authService.refreshToken().pipe(
        switchMap(response => {
          return next(addTokenToRequest(req, response.accessToken));
        }),
        catchError(refreshError => {
          authService.forceLogout();
          return throwError(() => refreshError);
        })
      );
    })
  );
};

function addTokenToRequest(req: HttpRequest<unknown>, token: string): HttpRequest<unknown> {
  return req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });
}


function isAuthEndpoint(url: string): boolean {
  const authEndpoints = [
    `${environment.apiUrl}/auth/login`,
    `${environment.apiUrl}/auth/refresh-token`
  ];

  return authEndpoints.some(endpoint => url.includes(endpoint));
}
