import { Component, OnInit } from '@angular/core';
import {MapService} from '../../../shared/services/map.service';

@Component({
  selector: 'app-create-rental-point-by-location',
  templateUrl: './create-rental-point-by-location.component.html',
  styleUrls: ['./create-rental-point-by-location.component.css']
})
export class CreateRentalPointByLocationComponent implements OnInit {

  constructor(private mapService: MapService) {}

  lat = 52.14052719475131;
  lng = 26.102992085204164;
  zoom = 5;
  address: string;

  onChooseLocation(event): void{
    this.lat = event.coords.lat;
    this.lng = event.coords.lng;
    /*document.getElementById('autocomplete').nodeValue = */
  }

  ngOnInit(): void {
    const autocomplete = new google.maps.places.Autocomplete(document.getElementById('autocomplete') as HTMLInputElement, {
      types: ['address']
    });
    google.maps.event.addListener(autocomplete, 'place_changed', () => {
      const place = autocomplete.getPlace();
      this.lat = place.geometry.location.lat();
      this.lng = place.geometry.location.lng();
      this.address = place.formatted_address;
      console.log(place.geometry.location.lat());
      console.log(place.geometry.location.lng());
      console.log(place.formatted_address);
    });
  }

}
