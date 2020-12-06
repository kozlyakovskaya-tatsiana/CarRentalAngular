import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CountyInfo} from '../../../shared/utils/filters/CountyInfo';
import {CityInfo} from '../../../shared/utils/filters/CityInfo';
import {CarFilter} from '../../../shared/utils/filters/CarFilter';


@Component({
  selector: 'app-filter-bar',
  templateUrl: './filter-bar.component.html',
  styleUrls: ['./filter-bar.component.css']
})
export class FilterBarComponent{

  @Input() countries: Array<CountyInfo>;
  @Input() marks: Array<string>;
  @Input() transmissions: Array<string>;
  @Input() carcases: Array<string>;

  @Output() countryChanged: EventEmitter<string> = new EventEmitter<string>();
  @Output() cityChanged: EventEmitter<string> = new EventEmitter<string>();
  @Output() filter: EventEmitter<CarFilter> = new EventEmitter<CarFilter>();

  cities: Array<CityInfo>;

  carFilter: CarFilter;

  constructor() {
    this.carFilter = new CarFilter();
    this.carFilter.marks = new Array<string>();
    this.carFilter.transmissions = new Array<string>();
  }

  onCountryChange(countryId: string): void{
    this.carFilter.countyId = countryId;
    this.countryChanged.emit(countryId);
  }

  onCityChange(cityId: string): void{
    this.carFilter.cityId = cityId;
    this.cityChanged.emit(cityId);
  }

  onMarkChange(event: any, mark: string): void{
    console.log(event);
    if (event.target.checked){
      console.log(event.target.checked);
      this.carFilter.marks.push(mark);
    }
    else{
      const index = this.carFilter.marks.indexOf(mark);
      if (index !== -1){
        this.carFilter.marks.splice(index, 1);
      }
    }
  }

  onTransmissionChange(event: any, transmission: string): void{
    if (event.target.checked){
      this.carFilter.transmissions.push(transmission);
    }
    else{
      const index = this.carFilter.transmissions.indexOf(transmission);
      if (index !== -1){
        this.carFilter.transmissions.splice(index, 1);
      }
    }
  }

  onFilter(): void{
    this.filter.emit(this.carFilter);
  }
}
