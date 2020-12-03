import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BookingInfoForRead} from '../../../shared/utils/booking/BookingInfoForRead';

@Component({
  selector: 'app-booking-card',
  templateUrl: './booking-car.component.html',
  styleUrls: ['./booking-car.component.css']
})
export class BookingCardComponent {

  @Input() bookingInfoForRead: BookingInfoForRead;

  @Output() clickApprove: EventEmitter<string> = new EventEmitter<string>();
  @Output() clickReject: EventEmitter<string> = new EventEmitter<string>();

  approve(bookingId: string): void{
    this.clickApprove.emit(bookingId);
  }

  reject(bookingId: string): void{
    this.clickReject.emit(bookingId);
  }

}
