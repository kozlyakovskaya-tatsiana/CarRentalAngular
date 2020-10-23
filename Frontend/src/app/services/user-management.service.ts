import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {UserBaseInfo} from '../utils/UserBaseInfo';

@Injectable({
  providedIn: 'root'
})
export class UserManagementService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/User/';

  public getUser(id: string): Observable<any> {
    return this.http.get(this.url + id);
  }

  public updateUserBaseInfo(user: UserBaseInfo): Observable<any>{
    return this.http.put(this.url, user);
  }
}
