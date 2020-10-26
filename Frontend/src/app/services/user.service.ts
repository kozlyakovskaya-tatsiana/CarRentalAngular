import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {UserBase} from '../utils/User/UserBase';
import {Headers} from '../utils/Authorize/Headers';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/user/';

  public updateUserBaseInfo(user: UserBase): Observable<any>{
    return this.http.put(this.url, user, { headers: Headers.authorizeHeader});
  }

}
