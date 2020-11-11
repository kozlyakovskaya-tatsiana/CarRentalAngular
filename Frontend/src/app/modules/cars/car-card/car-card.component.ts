import {Component, Input} from '@angular/core';
import {CarReadWithImage} from '../../../shared/utils/Car/CarReadWithImage';
import {CarSection} from '../../../shared/utils/Car/CarSection';
import {AuthorizeService} from '../../../shared/services/authorize.service';

@Component({
  selector: 'app-car-card',
  templateUrl: './car-card.component.html',
  styleUrls: ['./car-card.component.css']
})
export class CarCardComponent{

  constructor(public authService: AuthorizeService) {
    this.section = CarSection.Description;
  }

  CarSectionEnum = CarSection;

  @Input() car: CarReadWithImage;

  section: CarSection;
}
