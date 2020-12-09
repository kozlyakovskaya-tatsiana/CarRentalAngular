import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CarForSmallCard} from '../../../shared/utils/Car/CarForSmallCard';

@Component({
  selector: 'app-car-card-small',
  templateUrl: './car-card-small.component.html',
  styleUrls: ['./car-card-small.component.css']
})
export class CarCardSmallComponent {

  constructor() { }

  @Input() car: CarForSmallCard = undefined;
  @Input() btnInfoName: string;
  @Input() btnInfoLink: string;
  @Input() btnSimpleName: string;

  get isHidden(): boolean{
    return this.car?.status !== 'Free';
  }

  @Output() ClickSimpleBtn: EventEmitter<void> = new EventEmitter<void>();

  clickSimpleBtn(): void{
    this.ClickSimpleBtn.emit();
  }

}
