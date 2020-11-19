import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {CreateRentalPoint} from '../utils/rentalPoint/CreateRentalPoint';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RentalPointService {

  constructor(private http: HttpClient) {
  }

  private url = environment.baseApi + 'RentalPoint/';

  public createRentalPoint(point: CreateRentalPoint): Observable<any>{
    return this.http.post(this.url, point);
  }
}
