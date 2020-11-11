import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CarTechInfo} from '../utils/Car/CarTechInfo';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http: HttpClient) { }

  public backendUrlForImages = 'https://localhost:44397/images/';

  private url = 'https://localhost:44397/api/Car/';

  public getCars(): Observable<any>{
    return this.http.get(this.url + 'cars');
  }

  public getCarWithImages(id: string): Observable<any>{
    return this.http.get(this.url + id);
  }

  public getCarcases(): Observable<any>{
    return this.http.get(this.url + 'carcases');
  }

  public getFuelTypes(): Observable<any>{
    return this.http.get(this.url + 'fueltypes');
  }

  public getStatusTypes(): Observable<any>{
    return this.http.get(this.url + 'statustypes');
  }

  public getTransmissionsTypes(): Observable<any>{
    return this.http.get(this.url + 'transmissionstypes');
  }

  public getCarsForEditImages(id: string): Observable<any>{
    return this.http.get(this.url + 'edit/' + id);
  }

  public createCar(formData: FormData): Observable<any>{
    return this.http.post(this.url, formData);
  }

  public updateCarTechInfo(car: CarTechInfo): Observable<any>{
    return this.http.put(this.url + 'techinfo/', car);
  }

  public addCarImages(formData: FormData): Observable<any>{
    return this.http.post(this.url + 'addimages', formData);
  }

  public removeCar(id: string): Observable<any>{
    return this.http.delete(this.url + id);
  }

}
