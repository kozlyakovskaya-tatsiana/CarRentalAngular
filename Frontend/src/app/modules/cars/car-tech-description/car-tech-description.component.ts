import {Component, Input, OnInit} from '@angular/core';
import {CarReadWithImage} from '../../../shared/utils/Car/CarReadWithImage';

@Component({
  selector: 'app-car-tech-description',
  templateUrl: './car-tech-description.component.html',
  styleUrls: ['./car-tech-description.component.css']
})
export class CarTechDescriptionComponent{

  constructor() { }

  @Input() car: CarReadWithImage;

}
