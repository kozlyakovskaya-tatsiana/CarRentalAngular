import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CarInfo} from '../utils/Car/CarInfo';
import {CountyInfo} from '../utils/filters/CountyInfo';
import {CityInfo} from '../utils/filters/CityInfo';
import {CarFilter} from '../utils/filters/CarFilter';
import {CarForSmallCard} from '../utils/Car/CarForSmallCard';
import {PagedResponse} from '../utils/filters/PagedResponse';
import {RentalPointInfo} from '../utils/filters/RentalPointInfo';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http: HttpClient) { }

  public backendUrlForImages = 'https://localhost:44397/images/';

  private url = 'https://localhost:44397/api/Car/';

  public getCars(): Observable<any>{
    return this.http.get(this.url + 'tableinfo');
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

  public getCarsForSmallCards(): Observable<any>{
    return this.http.get(this.url + 'carsforsmallcards');
  }

  public getCarsCountries(): Observable<Array<CountyInfo>>{
    return this.http.get<Array<CountyInfo>>(this.url + 'countries');
  }

  public getCarsCities(countryId: string): Observable<Array<CityInfo>>{
    return this.http.get<Array<CityInfo>>(this.url + 'countries/' + countryId + '/cities');
  }

  public getCarsPoints(cityId: string): Observable<Array<RentalPointInfo>>{
    return this.http.get<Array<RentalPointInfo>>(this.url + 'cities/' + cityId + '/points');
  }

  public getCarsMarks(): Observable<Array<string>>{
    return this.http.get<Array<string>>(this.url + 'marks');
  }

  public createCar(formData: FormData): Observable<any>{
    return this.http.post(this.url, formData);
  }

  public updateCarTechInfo(car: CarInfo): Observable<any>{
    return this.http.put(this.url + 'info/', car);
  }

  public addCarImages(formData: FormData): Observable<any>{
    return this.http.post(this.url + 'addimages', formData);
  }

  public removeCar(id: string): Observable<any>{
    return this.http.delete(this.url + id);
  }

  public filterAndPaginateCars(filter: CarFilter): Observable<PagedResponse<CarForSmallCard>>{
    let params = new HttpParams();
    if (filter.countyId) {
      params = params.set('countryId', filter.countyId);
    }
    if (filter.cityId) {
      params = params.set('cityId', filter.cityId);
    }
    if (filter.rentalPointId) {
      params = params.set('rentalPointId', filter.rentalPointId);
    }
    filter.marks.forEach(mark => {
      params = params.append('marks', mark);
    });

    filter.carcases.forEach(carcase => {
      params = params.append('carcases', carcase);
    });

    filter.transmissions.forEach(transmission => {
      params = params.append('transmissions', transmission);
    });

    params = params.set('pageNumber', filter.pageNumber.toString());
    params = params.set('pageSize', filter.pageSize.toString());

    console.log(params);

    return this.http.get<PagedResponse<CarForSmallCard>>(this.url + 'filter/', {params});
  }

}
