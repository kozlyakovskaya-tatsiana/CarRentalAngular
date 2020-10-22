import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginRequest} from '../utils/LoginRequest';
import {Observable} from 'rxjs';
import {RegisterRequest} from '../utils/RegisterRequest';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/Account/';

  userEmail: string;

  get isAuthorized(): boolean {
    return !!localStorage.getItem('access_token');
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
  }
}
