import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {BookingRequest} from '../../../shared/utils/booking/BookingRequest';

@Component({
  selector: 'app-booking-flow',
  templateUrl: './booking-flow.component.html',
  styleUrls: ['./booking-flow.component.css']
})
export class BookingFlowComponent implements OnInit{

  constructor() {
    this.bookingRequest = new BookingRequest();
  }

  @Input() imageSrc: string;
  @Input() carName: string;
  @Input() carCostPerDay: number;

  @Output() clickSubmit: EventEmitter<BookingRequest> = new EventEmitter<BookingRequest>();

  bookingRequest: BookingRequest;
  bookingRequestForm: FormGroup;

  dataCorrectnessValidator(group: FormGroup): any{
    const start = group.get('startDate').value;
    const end = group.get('endDate').value;
    const startTime = new Date(start).getTime();
    const endTime = new Date(end).getTime();

    return endTime - startTime >= 0 ? null : { incorrectDates: true};

  }

  showModal(): void{
    document.getElementById('bookingFlowBtn').click();
  }

  get totalSum(): number{
    const start = new Date(this.bookingRequest.startDateOfRenting).getTime();
    const end = new Date(this.bookingRequest.endDateOfRenting).getTime();
    const oneDay = 24 * 60 * 60 * 1000;
    const daysAmount =  Math.round(Math.abs((end - start) / oneDay)) + 1;
    return daysAmount * this.carCostPerDay;
  }

  closeModal(): void{
    document.getElementById('closeBtn').click();
  }

  submit(request: BookingRequest): void{
    this.clickSubmit.emit(request);
  }

  ngOnInit(): void {
    this.bookingRequestForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(20)
      ]),
      surname: new FormControl('', [
        Validators.required,
        Validators.maxLength(20)
      ]),
      dateOfBirth: new FormControl(undefined, Validators.required),
      telephoneNumber: new FormControl('', [
        Validators.required,
        Validators.pattern(/\(?\d{3}\)?-? *\d{3}-? *-?\d{4}/)
      ]),
      passportId: new FormControl('', [
        Validators.required
      ]),
      passportSerialNumber: new FormControl('', Validators.required),
      startDate: new FormControl('', Validators.required),
      endDate: new FormControl('', Validators.required)
    }, {validators: this.dataCorrectnessValidator});
  }
}
