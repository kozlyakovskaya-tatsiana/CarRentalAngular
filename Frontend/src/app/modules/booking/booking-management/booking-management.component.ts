import { Component, OnInit } from '@angular/core';
import {BookingService} from '../../../shared/services/booking.service';
import {HttpResponseService} from '../../../shared/services/http-response.service';
import {BookingInfoForRead} from '../../../shared/utils/booking/BookingInfoForRead';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';

@Component({
  selector: 'app-booking-management',
  templateUrl: './booking-management.component.html',
  styleUrls: ['./booking-management.component.css']
})
export class BookingManagementComponent implements OnInit {

  constructor(private bookingService: BookingService,
              private httpResponseService: HttpResponseService) {

  }

  bookings$: Observable<Array<BookingInfoForRead>>;

  ngOnInit(): void {
    this.bookings$ = this.bookingService.getBookingList().pipe(
      catchError(err => {
        this.httpResponseService.showErrorMessage(err);
        return of(err);
      })
    );
  }

}
