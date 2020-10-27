import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Headers} from '../utils/Authorize/Headers';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/role/';

  public getRoles(): Observable<any>{
    return this.http.get(this.url + 'roles' , { headers: Headers.authorizeHeader} );
  }
}
