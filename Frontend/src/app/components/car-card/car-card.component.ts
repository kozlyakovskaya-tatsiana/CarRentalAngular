import {Component, Input, OnInit} from '@angular/core';
import {CarToRead} from '../../utils/Car/CarToRead';

@Component({
  selector: 'app-car-card',
  templateUrl: './car-card.component.html',
  styleUrls: ['./car-card.component.css']
})
export class CarCardComponent implements OnInit {

  constructor() {}

  @Input() car: CarToRead;

  ngOnInit(): void {
  }

}
