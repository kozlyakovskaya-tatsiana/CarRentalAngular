import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/Car/';

  public getCars(): Observable<any>{
    return this.http.get(this.url + 'cars');
  }

  public getCarcases(): Observable<any>{
    return this.http.get(this.url + 'carcases');
  }

  public getFuelTypes(): Observable<any>{
    return this.http.get(this.url + 'fueltypes');
  }

  public getTransmissionsTypes(): Observable<any>{
    return this.http.get(this.url + 'transmissionstypes');
  }

}