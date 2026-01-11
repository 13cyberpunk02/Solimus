import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap, catchError, throwError, BehaviorSubject } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  LoginRequest,
  LoginResponse,
  RefreshTokenRequest,
  AuthTokens,
  JwtPayload,
  RegistrationRequest
} from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);

  private readonly API_URL = environment.apiUrl;
  private readonly ACCESS_TOKEN_KEY = 'access_token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';

  private refreshTokenInProgress = false;
  private refreshTokenSubject = new BehaviorSubject<string | null>(null);

  private readonly _isAuthenticated = signal<boolean>(this.hasValidToken());
  private readonly _currentUser = signal<JwtPayload | null>(this.decodeToken());

  readonly isAuthenticated = this._isAuthenticated.asReadonly();
  readonly currentUser = this._currentUser.asReadonly();
  readonly userId = computed(() => this.currentUser()?.sub ?? null);

  get isRefreshing(): boolean {
    return this.refreshTokenInProgress;
  }

  get refreshToken$(): BehaviorSubject<string | null> {
    return this.refreshTokenSubject;
  }

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.API_URL}/auth/login`, credentials)
      .pipe(
        tap(response => this.handleAuthSuccess(response)),
        catchError(error => {
          console.error('Login error:', error);
          return throwError(() => error);
        })
      );
  }

  register(model: RegistrationRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.API_URL}/auth/register`, model)
    .pipe(
      tap(response => this.handleAuthSuccess(response)),
      catchError(error => {
        console.log("Register error: ", error);
        return throwError(() => error);
      })
    );
  }

  refreshToken(): Observable<LoginResponse> {
    const tokens = this.getTokens();

    if (!tokens) {
      return throwError(() => new Error('No tokens available'));
    }

    this.refreshTokenInProgress = true;

    const request: RefreshTokenRequest = {
      accessToken: tokens.accessToken,
      refreshToken: tokens.refreshToken
    };

    return this.http.post<LoginResponse>(`${this.API_URL}/auth/refresh-token`, request)
      .pipe(
        tap(response => {
          this.handleAuthSuccess(response);
          this.refreshTokenInProgress = false;
          this.refreshTokenSubject.next(response.accessToken);
        }),
        catchError(error => {
          this.refreshTokenInProgress = false;
          this.refreshTokenSubject.next(null);
          this.logout();
          return throwError(() => error);
        })
      );
  }

  logout(): void {
    const userId = this.userId();

    if (userId && this.getAccessToken()) {
      this.http.post(`${this.API_URL}/auth/logout/${userId}`, {})
        .pipe(
          catchError(error => {
            console.error('Logout error:', error);
            return throwError(() => error);
          })
        )
        .subscribe({
          complete: () => this.clearAuthData()
        });
    } else {
      this.clearAuthData();
    }
  }

  forceLogout(): void {
    this.clearAuthData();
  }

  getAccessToken(): string | null {
    return localStorage.getItem(this.ACCESS_TOKEN_KEY);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(this.REFRESH_TOKEN_KEY);
  }

  getTokens(): AuthTokens | null {
    const accessToken = this.getAccessToken();
    const refreshToken = this.getRefreshToken();

    if (!accessToken || !refreshToken) {
      return null;
    }

    return { accessToken, refreshToken };
  }

  hasValidToken(): boolean {
    const token = this.getAccessToken();

    if (!token) {
      return false;
    }

    const payload = this.decodeToken();

    if (!payload) {
      return false;
    }

    const now = Math.floor(Date.now() / 1000);
    return payload.exp > now + 30;
  }

  isTokenExpiringSoon(): boolean {
    const payload = this.decodeToken();

    if (!payload) {
      return true;
    }

    const now = Math.floor(Date.now() / 1000);
    const fiveMinutes = 5 * 60;

    return payload.exp - now < fiveMinutes;
  }

  decodeToken(): JwtPayload | null {
    const token = this.getAccessToken();

    if (!token) {
      return null;
    }

    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
          .join('')
      );

      return JSON.parse(jsonPayload);
    } catch (error) {
      console.error('Token decode error:', error);
      return null;
    }
  }

  private handleAuthSuccess(response: LoginResponse): void {
    this.setTokens(response);
    this._isAuthenticated.set(true);
    this._currentUser.set(this.decodeToken());
  }

  private setTokens(tokens: LoginResponse): void {
    localStorage.setItem(this.ACCESS_TOKEN_KEY, tokens.accessToken);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, tokens.refreshToken);
  }

  private clearAuthData(): void {
    localStorage.removeItem(this.ACCESS_TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    this._isAuthenticated.set(false);
    this._currentUser.set(null);
    this.router.navigate(['/login']);
  }
}
