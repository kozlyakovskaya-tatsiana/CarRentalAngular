import { google } from '@agm/core/services/google-maps-types';
import { Component, OnInit } from '@angular/core';
import {MapService} from '../../../shared/services/map.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  constructor(private mapService: MapService) {}

  lat = 52.14052719475131;
  lng = 26.102992085204164;
  geocoder: any;

  onChooseLocation(event): void{
    console.log(event);
  }

  ngOnInit(): void {
    this.mapService.geocodeReverse(this.lat, this.lng).subscribe(
      data => {
        console.log(data.results[0].formatted_address);
      },
      err => {
        console.log(err.message);
      });
  }

}
