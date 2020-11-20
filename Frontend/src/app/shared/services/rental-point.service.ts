import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {RentalPointLocationInfo} from '../utils/rentalPoint/RentalPointLocationInfo';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

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
    return this.http.get(this.url).pipe(
      map(data => data as Array<RentalPointLocationInfo>)
    );
  }
}
