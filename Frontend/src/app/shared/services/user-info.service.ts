import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/userinfo/';

  public getUser(id: string): Observable<any> {
    return this.http.get(this.url + id);
  }

}
