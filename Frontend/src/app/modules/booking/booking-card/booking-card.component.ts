import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BookingInfoForRead} from '../../../shared/utils/booking/BookingInfoForRead';
import {AuthorizeService} from '../../../shared/services/authorize.service';

@Component({
  selector: 'app-booking-card',
  templateUrl: './booking-car.component.html',
  styleUrls: ['./booking-car.component.css']
})
export class BookingCardComponent {

  constructor(public authorizeService: AuthorizeService) {}

  @Input() bookingInfoForRead: BookingInfoForRead;

  @Output() clickApprove: EventEmitter<string> = new EventEmitter<string>();
  @Output() clickReject: EventEmitter<string> = new EventEmitter<string>();
  @Output() clickClose: EventEmitter<string> = new EventEmitter<string>();

  approve(bookingId: string): void{
    this.clickApprove.emit(bookingId);
  }

  reject(bookingId: string): void{
    this.clickReject.emit(bookingId);
  }

  close(bookingId: string): void{
    this.clickClose.emit(bookingId);
  }

}
