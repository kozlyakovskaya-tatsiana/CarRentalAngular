import {Component, Input} from '@angular/core';
import {CarToReadWithImage} from '../../../shared/utils/Car/CarToReadWithImage';
import {CarSection} from '../../../shared/utils/Car/CarSection';

@Component({
  selector: 'app-car-card',
  templateUrl: './car-card.component.html',
  styleUrls: ['./car-card.component.css']
})
export class CarCardComponent{

  constructor() {
    this.section = CarSection.Description;
  }

  CarSectionEnum = CarSection;

  @Input() car: CarToReadWithImage;

  section: CarSection;
}
