import {Component, Input, OnInit} from '@angular/core';
import {CarToRead} from '../../utils/Car/CarToRead';

@Component({
  selector: 'app-tech-description',
  templateUrl: './car-tech-description.component.html',
  styleUrls: ['./car-tech-description.component.css']
})
export class CarTechDescriptionComponent{

  constructor() { }

  @Input() car: CarToRead;

}
