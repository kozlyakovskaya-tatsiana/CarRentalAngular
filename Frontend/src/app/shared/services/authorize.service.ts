import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginRequest} from '../utils/Authorize/LoginRequest';
import {Observable} from 'rxjs';
import {RegisterRequest} from '../utils/Authorize/RegisterRequest';
import {Router} from '@angular/router';
import {tap} from 'rxjs/operators';
import {Headers} from '../utils/Authorize/Headers';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {

  constructor(private http: HttpClient,
              private router: Router) {
  }

  private url = 'https://localhost:44397/api/Account/';

  private tokenUrl = 'https://localhost:44397/api/Token/';

  get userId(): string{
    return localStorage.getItem('user_id');
  }

  get userRole(): string{
    return localStorage.getItem('user_role');
  }

  get userEmail(): string{
    return localStorage.getItem('user_email');
  }

  get accessToken(): string{
    return localStorage.getItem('access_token');
  }

  get refreshToken(): string{
    return localStorage.getItem('refresh_token');
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

  get isManager(): boolean {
    return localStorage.getItem('user_role') === 'manager';
  }

  login(loginModel: LoginRequest): Observable<any>{
    return this.http.post(this.url + 'login', loginModel);
  }

  register(registerModel: RegisterRequest): Observable<any>{
    return this.http.post(this.url + 'register', registerModel);
  }

  logout(): void{
    this.router.navigate(['', 'login']);
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('user_id');
    localStorage.removeItem('user_role');
    localStorage.removeItem('user_email');
  }

  doRefreshToken(): Observable<any>{
    return this.http.post(this.tokenUrl + 'refresh', JSON.stringify(this.refreshToken), { headers: Headers.jsonHeader}).pipe(
      tap((tokens) => {
      localStorage.setItem('access_token', tokens.accessToken);
      localStorage.setItem('refresh_token', tokens.refreshToken);
      console.log(`Refresh token\n ${tokens} `);
    }));
  }
}
