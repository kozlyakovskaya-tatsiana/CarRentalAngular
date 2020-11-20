import { Component, OnInit } from '@angular/core';
import {MapService} from '../../../shared/services/map.service';
import {CreateRentalPoint} from '../../../shared/utils/rentalPoint/CreateRentalPoint';
import {RentalPointService} from '../../../shared/services/rental-point.service';
import {HttpResponseService} from '../../../shared/services/http-response.service';

@Component({
  selector: 'app-create-rental-point-by-location',
  templateUrl: './create-rental-point-by-location.component.html',
  styleUrls: ['./create-rental-point-by-location.component.css']
})
export class CreateRentalPointByLocationComponent implements OnInit {

  constructor(private mapService: MapService,
              private rentalPointService: RentalPointService,
              private httpResponseService: HttpResponseService){
    this.createRentalPoint = new CreateRentalPoint();
  }

  createRentalPoint: CreateRentalPoint;
  lat = 53.8983631;
  lng = 27.5538021;

  fullAddress = undefined;
  get fullAddressArray(): string[]{
    return this.fullAddress ? this.fullAddress.split(',') : new Array<string>();
  }

  get country(): string {
    return this.fullAddressArray[this.fullAddressArray.length - 1];
  }

  get city(): string {
    return this.fullAddressArray[1];
  }

  get address(): string {
    return this.fullAddressArray[0];
  }

  private resetMarkers(markers: google.maps.Marker[]): void{
    markers.forEach((marker) => {
      marker.setMap(null);
    });
    markers.length = 0;
  }

  private initMap(): void{
    const map = new google.maps.Map(
      document.getElementById('map') as HTMLElement,
      {
        center: {lat: this.lat, lng: this.lng},
        zoom: 6,
        mapTypeId: 'roadmap',
      }
    );
    const autocomplete = new google.maps.places.Autocomplete(document.getElementById('autocomplete') as HTMLInputElement, {
      types: ['address']
    });

    const markers: google.maps.Marker[] = [];

    google.maps.event.addListener(autocomplete, 'place_changed', () => {
      const place = autocomplete.getPlace();

      this.fullAddress = place.formatted_address;
      this.lat = place.geometry.location.lat();
      this.lng = place.geometry.location.lng();

      this.resetMarkers(markers);

      markers.push(
        new google.maps.Marker({
          map,
          title: place.name,
          position: place.geometry.location
        })
      );

      const bounds = new google.maps.LatLngBounds();
      if (place.geometry.viewport) {
        bounds.union(place.geometry.viewport);
      } else {
        bounds.extend(place.geometry.location);
      }
      map.fitBounds(bounds);
    });

    google.maps.event.addListener(map, 'click', (event) => {
      this.fullAddress = '';
      (document.getElementById('autocomplete')as HTMLInputElement).value = '';
      this.resetMarkers(markers);
      markers.push(new google.maps.Marker({
        position: event.latLng,
        map
      }));
      map.panTo(event.latLng);
      this.mapService.geocodeReverse(event.latLng.lat(), event.latLng.lng()).subscribe(
        data => {
          console.log(data);
          this.fullAddress = data?.results[0]?.formatted_address;
      },
        err => {
          console.log(err);
        }
      );
    });
  }

  onSubmit(): void{
    this.createRentalPoint.country = this.country;
    this.createRentalPoint.city = this.city;
    this.createRentalPoint.address = this.address;
    this.createRentalPoint.lat = this.lat;
    this.createRentalPoint.lng = this.lng;

    this.rentalPointService.createRentalPoint(this.createRentalPoint).subscribe(
      data => {
        console.log(data);
        this.httpResponseService.showSuccessMessage('Creating successful');
        window.location.reload();
    },
    err => {
        console.log(err);
        this.httpResponseService.showErrorMessage(err);
    });
  }

  ngOnInit(): void {
    this.initMap();
  }
}
