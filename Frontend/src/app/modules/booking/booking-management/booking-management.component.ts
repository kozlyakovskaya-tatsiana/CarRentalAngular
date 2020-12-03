import { Component, OnInit } from '@angular/core';
import {BookingService} from '../../../shared/services/booking.service';
import {HttpResponseService} from '../../../shared/services/http-response.service';
import {BookingInfoForRead} from '../../../shared/utils/booking/BookingInfoForRead';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {SwalService} from '../../../shared/services/swal.service';
import {AuthorizeService} from '../../../shared/services/authorize.service';

@Component({
  selector: 'app-booking-management',
  templateUrl: './booking-management.component.html',
  styleUrls: ['./booking-management.component.css']
})
export class BookingManagementComponent implements OnInit {

  constructor(private bookingService: BookingService,
              private httpResponseService: HttpResponseService,
              public authorizeService: AuthorizeService,
              private swalService: SwalService) {
  }

  bookings$: Observable<Array<BookingInfoForRead>>;
  statuses: Array<string>;

  approveBooking(bookingId: string): void{
    this.bookingService.approveBooking(bookingId).subscribe(
      data => {
        this.swalService.showSuccessMessage('Approving is successful').then(
          val => {
            window.location.reload();
          }
        );
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }

  rejectBookingByManager(bookingId: string): void{
    this.swalService.showWarningMessage('Do you really want to reject the booking?').then(
      res => {
        if (res.isConfirmed){
          this.bookingService.rejectBookingByManager(bookingId).subscribe(
            data => {
              this.swalService.showSuccessMessage('Rejecting is successful').then(
                val => {
                  window.location.reload();
                }
              );
            },
            err => {
              this.httpResponseService.showErrorMessage(err);
            }
          );
        }
      }
    );
  }

  rejectBookingByUser(bookingId: string): void{
    this.swalService.showWarningMessage('Do you really want to reject the booking?').then(
      res => {
        if (res.isConfirmed){
          this.bookingService.rejectBookingByUser(bookingId).subscribe(
            data => {
              this.swalService.showSuccessMessage('Rejecting is successful').then(
                val => {
                  window.location.reload();
                }
              );
            },
            err => {
              this.httpResponseService.showErrorMessage(err);
            }
          );
        }
      }
    );
  }

  getBookingByStatus(status: number): void{
    this.bookings$ = this.bookingService.getBookingsByStatus(status).pipe(
      catchError(err => {
        this.httpResponseService.showErrorMessage(err);
        return of(err);
      })
    );
  }

  getAllBookings(): void {
    this.bookings$ = this.bookingService.getBookingList().pipe(
      catchError(err => {
        this.httpResponseService.showErrorMessage(err);
        return of(err);
      })
    );
  }

  closeBooking(bookingId: string): void{
    this.bookingService.closeBooking(bookingId).subscribe(
      data => {
        this.swalService.showSuccessMessage('Closing is successful').then(
          val => {
            window.location.reload();
          }
        );
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }

  getAllUserBookings(): void {
    this.bookings$ = this.bookingService.getUserBookingList(this.authorizeService.userId).pipe(
      catchError(err => {
        this.httpResponseService.showErrorMessage(err);
        return of(err);
      })
    );
  }

  getAllUserBookingsByStatus(status: number): void {
    this.bookings$ = this.bookingService.getUserBookingListByStatus(this.authorizeService.userId, status).pipe(
      catchError(err => {
        this.httpResponseService.showErrorMessage(err);
        return of(err);
      })
    );
  }

  ngOnInit(): void {
    this.bookingService.getStatusList().subscribe(
      data => {
        this.statuses = data;
        console.log(data);
    },
      err => {
        this.httpResponseService.showErrorMessage(err);
    });
    if (this.authorizeService.isAdmin || this.authorizeService.isManager){
      this.getAllBookings();
    }
      else{
        this.getAllUserBookings();
      }
  }
}
