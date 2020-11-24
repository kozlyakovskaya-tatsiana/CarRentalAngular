import {Component, OnInit} from '@angular/core';
import {RentalPointCreateInfo} from '../../../shared/utils/rentalPoint/RentalPointCreateInfo';
import {RentalPointService} from '../../../shared/services/rental-point.service';
import {HttpResponseService} from '../../../shared/services/http-response.service';
import {SwalService} from '../../../shared/services/swal.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {RentalPointLocationInfo} from '../../../shared/utils/rentalPoint/RentalPointLocationInfo';
import {MapService} from '../../../shared/services/map.service';

@Component({
  selector: 'app-rental-edit',
  templateUrl: './rental-edit.component.html',
  styleUrls: ['./rental-edit.component.css']
})
export class RentalEditComponent implements OnInit {

  constructor(private rentalPointService: RentalPointService,
              private httpResponseService: HttpResponseService,
              private swalService: SwalService,
              private mapService: MapService,
              private activatedRoute: ActivatedRoute) {
    this.createRentalPoint = new RentalPointCreateInfo();
    this.currentPoint = new RentalPointLocationInfo();
    this.id = this.activatedRoute.snapshot.params.id;
  }

  id: string;
  fullAddress: string;
  currentPoint: RentalPointLocationInfo;
  rentalPointForm: FormGroup;

  createRentalPoint: RentalPointCreateInfo;

  /*get fullAddressArray(): string[]{
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
  }*/

  private initMap(): void{
    const map = new google.maps.Map(
      document.getElementById('map-edit') as HTMLElement,
      {
        center: {lat: this.currentPoint.lat, lng: this.currentPoint.lng},
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

    const markerCar = new google.maps.Marker({
        map,
        title: `${this.currentPoint.country}, ${this.currentPoint.city}, ${this.currentPoint.address}`,
        position: new google.maps.LatLng(this.currentPoint.lat, this.currentPoint.lng),
        icon: carIcon,
        animation: google.maps.Animation.DROP,
        draggable: true
      });

    google.maps.event.addListener(autocomplete, 'place_changed', () => {
      const place = autocomplete.getPlace();

      this.currentPoint.address = place.formatted_address;
      this.currentPoint.lat = place.geometry.location.lat();
      this.currentPoint.lng = place.geometry.location.lng();

      markerCar.setMap(null);
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
      this.currentPoint.address = '';
      (document.getElementById('autocomplete')as HTMLInputElement).value = '';

      markerCar.setMap(null);
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
          this.fullAddress = data?.results[0]?.formatted_address;
        },
        err => {
          console.log(err);
        }
      );

      this.currentPoint.lat = event.latLng.lat();
      this.currentPoint.lng = event.latLng.lng();

      if (map.getZoom() < 17)
      {
        map.setZoom(map.getZoom() + 1);
      }
      map.setCenter(marker.getPosition() as google.maps.LatLng);
    });
  }

  onSubmit(): void{
    /*this.createRentalPoint.country = this.country;
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
      });*/
  }

  ngOnInit(): void {
    this.rentalPointService.getRentalPointLocation(this.id).subscribe(
      data => {
        console.log(data);
        this.currentPoint = data;
        this.fullAddress = `${data.address}, ${data.city}, ${data.country}`;
        this.initMap();
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );

    this.rentalPointForm = new FormGroup({
      name: new FormControl('', Validators.required)
    });
  }

}
