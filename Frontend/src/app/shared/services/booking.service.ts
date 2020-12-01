import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {BookingRequest} from '../utils/booking/BookingRequest';
import {Observable} from 'rxjs';
import {BookingInfoForRead} from '../utils/booking/BookingInfoForRead';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private http: HttpClient) { }

  private url = environment.baseApi + 'booking/';

  public bookCar(bookingRequest: BookingRequest): Observable<any>{
    return this.http.post(this.url, bookingRequest);
  }

  public getBookingList(): Observable<Array<BookingInfoForRead>>{
    return this.http.get(this.url + 'list').pipe(
      map(list => {
        const books = list as Array<BookingInfoForRead>;
        books.forEach(book => {
          book.carImageName = environment.backendUrlForImages + book.carImageName;
          book.startDateOfRenting = new Date(book.startDateOfRenting).toISOString().split('T')[0];
          book.endDateOfRenting = new Date(book.endDateOfRenting).toISOString().split('T')[0];
          book.personDateOfBirth = new Date(book.personDateOfBirth).toISOString().split('T')[0];
      });
        return books;
      })
    );
  }

}
