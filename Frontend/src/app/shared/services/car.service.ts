import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CarToCreate} from '../utils/Car/CarToCreate';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44397/api/Car/';

  public getCars(): Observable<any>{
    return this.http.get(this.url + 'cars');
  }

  public getCar(id: string): Observable<any>{
    return this.http.get(this.url + id);
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

  public createCar(carToCreate: CarToCreate): Observable<any>{
    return this.http.post(this.url, carToCreate);
  }

  public removeCar(id: number): Observable<any>{
    return this.http.delete(this.url + id);
  }

}
