import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { RegisterRequest } from '../../../Models/Requests/Authentication/register';
import { map, Observable } from 'rxjs';
import { MainErrorResponse } from '../../../Models/Reponse/mainErrorResponse';
import { LoginRequest } from '../../../Models/Requests/Authentication/login';
import { MainSuccessResponse } from '../../../Models/Reponse/mainSuccessReponse';
import { ConfirmEmailRequest } from '../../../Models/Requests/Authentication/confirmEmail';
import { ResetPasswordRequest } from '../../../Models/Requests/Authentication/resetPassword';
import { jwtDecode } from 'jwt-decode';
import { RefreshTokenRequest } from '../../../Models/Requests/Authentication/refreshToken';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private userKey = 'token';
  private apiUrl = environment.apiUrl;
  constructor(private httpClient: HttpClient) { }

  login(request: LoginRequest): Observable<MainErrorResponse | MainSuccessResponse> {
    return this.httpClient.post<MainErrorResponse | MainSuccessResponse>(`${this.apiUrl}/identity/login`, request).pipe(
      map((response) => {        
        const res = response as MainSuccessResponse;        
        if(!res.isFailure) {
          localStorage.setItem(this.userKey, JSON.stringify(res.value));
        }
        return response;
      })
    );
  }

  register(request: RegisterRequest): Observable<MainErrorResponse | MainSuccessResponse> {
    return this.httpClient.post<MainErrorResponse | MainSuccessResponse>(`${this.apiUrl}/identity/registration`, request);
  }

  confirmEmail(request: ConfirmEmailRequest): Observable<MainErrorResponse | MainSuccessResponse> {
    return this.httpClient.put<MainErrorResponse | MainSuccessResponse>(`${this.apiUrl}/identity/confirm-email`, request);
  }

  forgotPassword(email: string): Observable<MainErrorResponse | MainSuccessResponse> {
    return this.httpClient.post<MainErrorResponse | MainSuccessResponse>(`${this.apiUrl}/identity/forgot-password?email=${email}`, {});
  }

  resetPassword(request: ResetPasswordRequest): Observable<MainErrorResponse | MainSuccessResponse> {
    return this.httpClient.put<MainErrorResponse | MainSuccessResponse>(`${this.apiUrl}/identity/reset-password`, request);
  }

  refreshAccessToken(request: RefreshTokenRequest): Observable<MainErrorResponse | MainSuccessResponse> {
    return this.httpClient.put<MainErrorResponse | MainSuccessResponse>(` ${this.apiUrl}/identity/refresh-token`, request);
  }

  logout = (): void => {
    localStorage.removeItem(this.userKey);
  }

  getRefreshTokenModel(): RefreshTokenRequest | null {
    const tokens = localStorage.getItem(this.userKey);
    if(!tokens) return null;
    const tokenDetail: RefreshTokenRequest = JSON.parse(tokens);
    return tokenDetail;
  }

  isLoggedIn() {
    return this.getToken() ? true : false;
  }

  getCurrentUserRoles = (): string[] | null => {
    const token = this.getToken();
    if (!token) return null;
    const decodedToken: any = jwtDecode(token);
    return decodedToken.role || null;
  }

  getToken = (): string | null => {
    const token = localStorage.getItem(this.userKey);    
    if (!token) return null;
    const result:  RefreshTokenRequest = JSON.parse(token);
    return result.accessToken;
  }
}
