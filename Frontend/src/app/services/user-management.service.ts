import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserManagementService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/User/';

  public getUser(email: string): Observable<any>{
    return this.http.get(this.url + email);
  }
}
