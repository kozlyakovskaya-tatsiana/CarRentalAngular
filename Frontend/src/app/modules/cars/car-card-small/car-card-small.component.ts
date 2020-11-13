import {Component, Input, OnInit} from '@angular/core';
import {CarForSmallCard} from '../../../shared/utils/Car/CarForSmallCard';

@Component({
  selector: 'app-car-card-small',
  templateUrl: './car-card-small.component.html',
  styleUrls: ['./car-card-small.component.css']
})
export class CarCardSmallComponent implements OnInit {

  constructor() { }

  @Input() car: CarForSmallCard;
  @Input() btnName: string;
  @Input() btnLink: string;

  ngOnInit(): void {
  }

}
