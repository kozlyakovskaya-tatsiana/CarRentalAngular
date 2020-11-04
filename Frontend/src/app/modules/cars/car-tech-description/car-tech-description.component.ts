import {Component, Input, OnInit} from '@angular/core';
import {CarToReadWithImage} from '../../../shared/utils/Car/CarToReadWithImage';

@Component({
  selector: 'app-tech-description',
  templateUrl: './car-tech-description.component.html',
  styleUrls: ['./car-tech-description.component.css']
})
export class CarTechDescriptionComponent{

  constructor() { }

  @Input() car: CarToReadWithImage;

}
