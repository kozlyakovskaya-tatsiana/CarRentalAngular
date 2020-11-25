import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {RentalPointService} from '../../../shared/services/rental-point.service';
import {RentalPointLocationInfo} from '../../../shared/utils/rentalPoint/RentalPointLocationInfo';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-rental-points-map',
  templateUrl: './rental-points-map.component.html',
  styleUrls: ['./rental-points-map.component.css']
})
export class RentalPointsMapComponent implements OnInit {

  constructor(private rentalPointService: RentalPointService) { }

  locations$: Observable<Array<RentalPointLocationInfo>>;
  location: RentalPointLocationInfo;

  onClickMarker(location: RentalPointLocationInfo): void{
    this.location = location;
  }

  ngOnInit(): void {
    this.locations$ = this.rentalPointService.getRentalPointLocations();
  }

}
