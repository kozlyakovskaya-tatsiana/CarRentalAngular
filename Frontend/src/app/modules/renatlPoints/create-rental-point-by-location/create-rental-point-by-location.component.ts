import { Component, OnInit } from '@angular/core';
import {MapService} from '../../../shared/services/map.service';
import {RentalPointLocationInfo} from '../../../shared/utils/rentalPoint/RentalPointLocationInfo';
import {RentalPointService} from '../../../shared/services/rental-point.service';
import {HttpResponseService} from '../../../shared/services/http-response.service';
import {environment} from '../../../../environments/environment';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {RentalPointCreateInfo} from '../../../shared/utils/rentalPoint/RentalPointCreateInfo';
import {SwalService} from '../../../shared/services/swal.service';

@Component({
  selector: 'app-create-rental-point-by-location',
  templateUrl: './create-rental-point-by-location.component.html',
  styleUrls: ['./create-rental-point-by-location.component.css']
})
export class CreateRentalPointByLocationComponent implements OnInit {

  constructor(private mapService: MapService,
              private rentalPointService: RentalPointService,
              private httpResponseService: HttpResponseService,
              private swalService: SwalService){
    this.createRentalPoint = new RentalPointCreateInfo();
    this.lat = environment.defaultLat;
    this.lng = environment.defaultLng;
  }

  rentalPointForm: FormGroup;

  createRentalPoint: RentalPointCreateInfo;
  lat: number;
  lng: number;

  existingPoints: RentalPointLocationInfo[];

  fullAddress: string;

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

    let marker: google.maps.Marker;

    const carIcon = {
      url: 'assets/car-icon.png',
      scaledSize: new google.maps.Size(30, 30), // scaled size
      origin: new google.maps.Point(0, 0), // origin
      anchor: new google.maps.Point(0, 0) // anchor
    };

    this.existingPoints?.forEach((point) => {
        const markerCar = new google.maps.Marker({
          map,
          title: `${point.country}, ${point.city}, ${point.address}`,
          position: new google.maps.LatLng(point.lat, point.lng),
          icon: carIcon,
          animation: google.maps.Animation.DROP
        });
    });

    google.maps.event.addListener(autocomplete, 'place_changed', () => {
      const place = autocomplete.getPlace();

      this.fullAddress = place.formatted_address;
      this.rentalPointForm.patchValue({
        fullAddress: this.fullAddress
      });
      this.lat = place.geometry.location.lat();
      this.lng = place.geometry.location.lng();

      marker?.setMap(null);

      marker = null;

      marker = new google.maps.Marker({
        map,
        title: place.name,
        position: place.geometry.location,
        animation: google.maps.Animation.BOUNCE
      });

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

      marker?.setMap(null);
      marker = null;

      marker = new google.maps.Marker({
        map,
        position: event.latLng,
        animation: google.maps.Animation.BOUNCE
      });

      map.panTo(event.latLng);
      this.mapService.geocodeReverse(event.latLng.lat(), event.latLng.lng()).subscribe(
        data => {
          console.log(data);
          this.fullAddress = data?.results[0]?.formatted_address;
          this.rentalPointForm.patchValue({
            fullAddress: this.fullAddress
          });
      },
        err => {
          console.log(err);
        }
      );
      this.lat = event.latLng.lat();
      this.lng = event.latLng.lng();

      if (map.getZoom() < 17)
      {
        map.setZoom(map.getZoom() + 1);
      }
      map.setCenter(marker.getPosition() as google.maps.LatLng);
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
        this.swalService.showSuccessMessage('Creating successful')
          .then(val => {
            window.location.reload();
            }
          );
    },
    err => {
        console.log(err);
        this.httpResponseService.showErrorMessage(err);
    });
  }

  ngOnInit(): void {
    this.rentalPointService.getRentalPointLocations().subscribe(
      data => {
        this.existingPoints = data;
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
        this.initMap();
      },
      () => {
        console.log(this.existingPoints);
        this.initMap();
      }
    );

    this.rentalPointForm = new FormGroup({
      name: new FormControl('', Validators.required),
      fullAddress: new FormControl('', Validators.required),
    });
  }
}
