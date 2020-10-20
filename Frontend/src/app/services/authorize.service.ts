import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginModel} from '../utils/LoginModel';
import {Observable} from 'rxjs';
import {RegisterModel} from '../utils/RegisterModel';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/Account/';

  userEmail: string;

  userRole: string;

  get isAuthorized(): boolean {
    return Boolean(this.userEmail);
  }

  login(loginModel: LoginModel): Observable<any>{
    return this.http.post(this.url + 'login', loginModel);
  }
  register(registerModel: RegisterModel): Observable<any>{
    return this.http.post(this.url + 'register', registerModel);
  }
}
