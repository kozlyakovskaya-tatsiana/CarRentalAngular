import {Component, Input} from '@angular/core';
import {CarToReadWithImage} from '../../../shared/utils/Car/CarToReadWithImage';

export enum Section{
  Description,
  Photo,
  RentalConditions
}
@Component({
  selector: 'app-car-card',
  templateUrl: './car-card.component.html',
  styleUrls: ['./car-card.component.css']
})
export class CarCardComponent{

  constructor() {
    this.section = Section.Description;
  }
  SectionEnum = Section;

  @Input() car: CarToReadWithImage;

  section: Section;
}
