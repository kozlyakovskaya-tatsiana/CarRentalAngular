import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Headers} from '../utils/Authorize/Headers';
import {EditUser} from '../utils/User/EditUser';
import {UserCreate} from '../utils/User/UserCreate';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/admin/';

  public createUser(userToCreate: UserCreate): Observable<any>{
    return this.http.post(this.url, userToCreate, { headers: Headers.authorizeHeader});
  }

  public getAllUsers(): Observable<any>{
    return this.http.get(this.url + 'users', { headers: Headers.authorizeHeader} );
  }

  public updateUser(userToUpdate: EditUser): Observable<any>{
    return this.http.put (this.url, userToUpdate, { headers: Headers.authorizeHeader});
  }

  public deleteUser(id: string): Observable<any>{
    return this.http.delete(this.url + id, {headers : Headers.authorizeHeader});
  }
}
