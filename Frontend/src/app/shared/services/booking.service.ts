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
    return this.http.post(this.url + 'book', bookingRequest);
  }

  public approveBooking(bookingId: string): Observable<any>{
    return this.http.get(this.url + 'approve/' + bookingId);
  }

  public closeBooking(bookingId: string): Observable<any>{
    return this.http.get(this.url + 'close/' + bookingId);
  }

  public rejectBookingByManager(bookingId: string): Observable<any>{
    return this.http.get(this.url + 'reject/' + bookingId);
  }

  public rejectBookingByUser(bookingId: string): Observable<any>{
    return this.http.get(this.url + 'userreject/' + bookingId);
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

  public getStatusList(): Observable<Array<string>>{
    return this.http.get<Array<string>>(this.url + 'statuses');
  }

  public getBookingsByStatus(status: number): Observable<Array<BookingInfoForRead>>{
    return this.http.get<Array<BookingInfoForRead>>(this.url + 'list/' + status).pipe(
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

  public getUserBookingList(userId: string): Observable<Array<BookingInfoForRead>>{
    return this.http.get(this.url + 'user/' + userId + '/list').pipe(
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

  public getUserBookingListByStatus(userId: string, status: number): Observable<Array<BookingInfoForRead>>{
    return this.http.get(this.url + 'user/' + userId + '/list/' + status).pipe(
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
