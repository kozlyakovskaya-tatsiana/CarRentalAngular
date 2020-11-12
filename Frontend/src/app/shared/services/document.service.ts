import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/document/';

  public deleteDocument(id: string): Observable<any>{
    return this.http.delete(this.url + id);
  }
}
