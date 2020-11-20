import {Component, EventEmitter, Input, Output} from '@angular/core';
import {RentalPointTableInfo} from '../../../shared/utils/rentalPoint/RentalPointTableInfo';

@Component({
  selector: 'app-rental-point-table',
  templateUrl: './rental-point-table.component.html',
  styleUrls: ['./rental-point-table.component.css']
})
export class RentalPointTableComponent {

  constructor() { }

  @Input() rentalPoints: Array<RentalPointTableInfo>;

  @Output() deletePoint: EventEmitter<string> = new EventEmitter<string>();

  onDelete(id): void{
    alert('in child');
    this.deletePoint.emit(id);
  }
}
