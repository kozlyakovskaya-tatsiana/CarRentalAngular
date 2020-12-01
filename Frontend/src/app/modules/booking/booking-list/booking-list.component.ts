import {Component, Input} from '@angular/core';
import {BookingInfoForRead} from '../../../shared/utils/booking/BookingInfoForRead';

@Component({
  selector: 'app-booking-list',
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']
})
export class BookingListComponent {

  @Input() bookingInfoForRead: BookingInfoForRead;

}
