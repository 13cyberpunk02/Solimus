import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { MainSuccessResponse } from '../../Models/Reponse/mainSuccessReponse';
import { MainErrorResponse } from '../../Models/Reponse/mainErrorResponse';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private apiUrl = environment.apiUrl;
  constructor(private httpClient: HttpClient) { }

  getAllUsers(): Observable<MainErrorResponse | MainSuccessResponse> {
    return this.httpClient.get<MainErrorResponse | MainSuccessResponse>(`${this.apiUrl}/account/get-all-users`);
  }

  getUserByEmail(): Observable<MainErrorResponse | MainSuccessResponse> {
    return this.httpClient.get<MainErrorResponse | MainSuccessResponse>(`${this.apiUrl}/account/get-user-by-email`);
  }

  getUserById(id: string): Observable<MainErrorResponse | MainSuccessResponse> {
    return this.httpClient.get<MainErrorResponse | MainSuccessResponse>(`${this.apiUrl}/account/get-user-by-id?id=${id}`);
  }
}
