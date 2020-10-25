import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Headers} from '../utils/Headers';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/admin/';

  public getAllUsers(): Observable<any>{
    return this.http.get(this.url + 'users', { headers: Headers.authorizeHeader} );
  }
}
