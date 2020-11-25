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
  @Output() editPoint: EventEmitter<string> = new EventEmitter<string>();
  @Output() getInfoAboutPoint: EventEmitter<string> = new EventEmitter<string>();

  onDelete(id: string): void{
    this.deletePoint.emit(id);
  }
  onEdit(id: string): void{
    this.editPoint.emit(id);
  }
  onGetInfo(id: string): void{
    this.getInfoAboutPoint.emit(id);
  }
}
