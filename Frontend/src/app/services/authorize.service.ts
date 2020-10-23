import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {LoginRequest} from '../utils/LoginRequest';
import {Observable} from 'rxjs';
import {RegisterRequest} from '../utils/RegisterRequest';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {

  constructor(private http: HttpClient) {
  }

  private url = 'https://localhost:44397/api/Account/';

  get userId(): string{
    return localStorage.getItem('user_id');
  }

  get userRole(): string{
    return localStorage.getItem('user_role');
  }

  get userEmail(): string{
    return localStorage.getItem('user_email');
  }

  get isAuthorized(): boolean {
    return !!localStorage.getItem('access_token');
  }

  get isAdmin(): boolean {
    return localStorage.getItem('user_role') === 'admin';
  }

  get isUser(): boolean {
    return localStorage.getItem('user_role') === 'user';
  }

  login(loginModel: LoginRequest): Observable<any>{
    return this.http.post(this.url + 'login', loginModel);
  }

  register(registerModel: RegisterRequest): Observable<any>{
    return this.http.post(this.url + 'register', registerModel);
  }

  logout(): void{
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('user_id');
    localStorage.removeItem('user_role');
    localStorage.removeItem('user_email');
  }

}
