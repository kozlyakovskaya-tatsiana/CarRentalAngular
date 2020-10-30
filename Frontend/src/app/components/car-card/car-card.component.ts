import {Component, Input, OnInit} from '@angular/core';
import {CarToRead} from '../../utils/Car/CarToRead';

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
export class CarCardComponent implements OnInit {

  constructor() {
    this.section = Section.Description;
  }
  SectionEnum = Section;

  @Input() car: CarToRead;

  section: Section;

  ngOnInit(): void {
  }

}
