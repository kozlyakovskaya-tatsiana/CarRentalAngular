import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {BookingRequest} from '../utils/booking/BookingRequest';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private http: HttpClient) { }

  private url = environment.baseApi + 'booking';

  public bookCar(bookingRequest: BookingRequest): Observable<any>{
    return this.http.post(this.url, bookingRequest);
  }

}
