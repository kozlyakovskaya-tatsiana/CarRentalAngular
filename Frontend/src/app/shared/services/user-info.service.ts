import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {UserReadInfo} from '../utils/User/UserReadInfo';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/userinfo/';

  public getUser(id: string): Observable<UserReadInfo> {
    return this.http.get(this.url + id).pipe(
      map(data => {
        const user = data as UserReadInfo;
        user.dateOfBirth = new Date(user.dateOfBirth).toISOString().split('T')[0];
        return user;
      })
    );
  }

}
