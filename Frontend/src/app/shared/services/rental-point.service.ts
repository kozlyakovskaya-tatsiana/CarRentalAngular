import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {RentalPointLocationInfo} from '../utils/rentalPoint/RentalPointLocationInfo';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {RentalPointTableInfo} from '../utils/rentalPoint/RentalPointTableInfo';

@Injectable({
  providedIn: 'root'
})
export class RentalPointService {

  constructor(private http: HttpClient) {
  }

  private url = environment.baseApi + 'RentalPoint/';

  public createRentalPoint(point: RentalPointLocationInfo): Observable<any>{
    return this.http.post(this.url, point);
  }

  public getRentalPointLocations(): Observable<RentalPointLocationInfo[]>{
    return this.http.get(this.url + 'locations').pipe(
      map(data => data as Array<RentalPointLocationInfo>)
    );
  }

  public getRentalPointTableInfo(): Observable<RentalPointTableInfo[]>{
    return this.http.get(this.url + 'tableinfo').pipe(
      map(data => data as Array<RentalPointTableInfo>)
    );
  }

  public deleteRentalPoint(id: string): Observable<any>{
    return this.http.delete(this.url + id);
  }

  public getRentalPointsNames(): Observable<Array<string>>{
    return this.http.get(this.url + 'names').pipe(
      map(data => data as Array<string>)
    );
  }
}
