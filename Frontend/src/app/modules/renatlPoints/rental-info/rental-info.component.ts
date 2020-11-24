import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {RentalPointLocationInfo} from '../../../shared/utils/rentalPoint/RentalPointLocationInfo';

@Component({
  selector: 'app-rental-info',
  templateUrl: './rental-info.component.html',
  styleUrls: ['./rental-info.component.css']
})
export class RentalInfoComponent{

  constructor() { }

  @Input() modalTitle: string;
  @Input() centerLat: number = environment.defaultLat;
  @Input() centerLng: number = environment.defaultLng;
  @Input() iconUrl: string;
  @Input() location: RentalPointLocationInfo = new RentalPointLocationInfo();
  @Input() address: string;
  @Input() btnName: string;

  @Output() clickConfirmBtn: EventEmitter<string> = new EventEmitter<string>();

  public showModal(): void{
    document.getElementById('infoBtnForModal').click();
  }

  public closeModal(): void{
    document.getElementById('closeBtn').click();
  }

  public clickBtn(): void{
    const id = this.location?.id;
    this.clickConfirmBtn.emit(id);
  }
}
