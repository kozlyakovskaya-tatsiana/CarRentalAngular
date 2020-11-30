import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {RentalPointLocationInfo} from '../utils/rentalPoint/RentalPointLocationInfo';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {RentalPointTableInfo} from '../utils/rentalPoint/RentalPointTableInfo';
import {RentalPointCreateInfo} from '../utils/rentalPoint/RentalPointCreateInfo';
import {RentalPointEditInfo} from '../utils/rentalPoint/RentalPointEditInfo';
import {CarForSmallCard} from '../utils/Car/CarForSmallCard';
import {Guid} from 'guid-typescript';

@Injectable({
  providedIn: 'root'
})
export class RentalPointService {

  constructor(private http: HttpClient) {
  }

  private url = environment.baseApi + 'RentalPoint/';

  public getRentalPointLocations(): Observable<RentalPointLocationInfo[]>{
    return this.http.get(this.url + 'locations').pipe(
      map(data => data as Array<RentalPointLocationInfo>)
    );
  }

  public getRentalPointLocation(id: string): Observable<RentalPointLocationInfo>{
    return this.http.get(this.url + 'location/' + id).pipe(
      map(data => data as RentalPointLocationInfo)
    );
  }

  public getRentalPointTableInfo(): Observable<RentalPointTableInfo[]>{
    return this.http.get(this.url + 'tableinfo').pipe(
      map(data => data as Array<RentalPointTableInfo>)
    );
  }

  public getRentalPointsNames(id = ''): Observable<Array<string>>{
    return this.http.get(this.url + 'names/' + id).pipe(
      map(data => data as Array<string>)
    );
  }

  public getRentalPointCars(pointId: string): Observable<Array<CarForSmallCard>>{
    return this.http.get(this.url + 'cars/' + pointId).pipe(
      map(data => data as Array<CarForSmallCard>)
    );
  }

  public createRentalPoint(point: RentalPointCreateInfo): Observable<any>{
    return this.http.post(this.url, point);
  }

  public editRentalPoint(point: RentalPointEditInfo): Observable<any>{
    return this.http.put(this.url, point);
  }

  public deleteRentalPoint(id: string): Observable<any>{
    return this.http.delete(this.url + id);
  }
}
