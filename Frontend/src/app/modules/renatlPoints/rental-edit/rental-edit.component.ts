import {Component, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {RentalPointService} from '../../../shared/services/rental-point.service';
import {HttpResponseService} from '../../../shared/services/http-response.service';
import {SwalService} from '../../../shared/services/swal.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {RentalPointLocationInfo} from '../../../shared/utils/rentalPoint/RentalPointLocationInfo';
import {MapService} from '../../../shared/services/map.service';
import {environment} from '../../../../environments/environment';
import {RentalPointEditInfo} from '../../../shared/utils/rentalPoint/RentalPointEditInfo';
import {Location} from '@angular/common';

@Component({
  selector: 'app-rental-edit',
  templateUrl: './rental-edit.component.html',
  styleUrls: ['./rental-edit.component.css']
})
export class RentalEditComponent implements OnInit{

  constructor(private rentalPointService: RentalPointService,
              private httpResponseService: HttpResponseService,
              private swalService: SwalService,
              private mapService: MapService,
              private activatedRoute: ActivatedRoute,
              private location: Location) {
    this.editRentalPoint = new RentalPointEditInfo();
    this.currentPoint = new RentalPointLocationInfo();
    this.id = this.activatedRoute.snapshot.params.id;
  }

  id: string;
  fullAddress: string;
  lat: number = environment.defaultLat;
  lng: number = environment.defaultLng;
  rentalName: string;
  currentPoint: RentalPointLocationInfo;
  rentalPointForm: FormGroup;

  editRentalPoint: RentalPointEditInfo;

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
      document.getElementById('map-edit') as HTMLElement,
      {
        center: {lat: this.lat, lng: this.lng},
        zoom: 6,
        mapTypeId: 'roadmap',
      }
    );
    const autocomplete = new google.maps.places.Autocomplete(document.getElementById('autocomplete') as HTMLInputElement, {
      types: ['address']
    });
    document.getElementById('autocomplete').addEventListener('keydown', event => {
      if (event.key === 'Enter'){
        event.preventDefault();
      }
    });

    const carIcon = {
      url: 'assets/car-icon.png',
      scaledSize: new google.maps.Size(30, 30), // scaled size
      origin: new google.maps.Point(0, 0), // origin
      anchor: new google.maps.Point(0, 0) // anchor
    };

    let markerCar = new google.maps.Marker({
        map,
        title: this.fullAddress,
        position: new google.maps.LatLng(this.lat, this.lng),
        icon: carIcon,
        animation: google.maps.Animation.DROP
      });

    google.maps.event.addListener(autocomplete, 'place_changed', () => {
      const place = autocomplete.getPlace();

      this.fullAddress = place.formatted_address;
      this.rentalPointForm.patchValue({
        fullAddress: this.fullAddress
      });
      this.lat = place.geometry.location.lat();
      this.lng = place.geometry.location.lng();

      markerCar.setMap(null);

      markerCar = new google.maps.Marker({
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

      markerCar.setMap(null);

      markerCar = new google.maps.Marker({
        map,
        position: event.latLng,
        animation: google.maps.Animation.BOUNCE
      });

      map.panTo(event.latLng);
      this.mapService.geocodeReverse(event.latLng.lat(), event.latLng.lng()).subscribe(
        data => {
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
      map.setCenter(markerCar.getPosition() as google.maps.LatLng);

    });
  }

  onSubmit(): void{
    this.editRentalPoint.id = this.id;
    this.editRentalPoint.country = this.country;
    this.editRentalPoint.city = this.city;
    this.editRentalPoint.address = this.address;
    this.editRentalPoint.lat = this.lat;
    this.editRentalPoint.lng = this.lng;
    this.editRentalPoint.name = this.rentalName;
    console.log(this.editRentalPoint);

    this.rentalPointService.editRentalPoint(this.editRentalPoint).subscribe(
      data => {
        this.swalService.showSuccessMessage('Updating successful')
          .then(val => {
              this.location.back();
            }
          );
      },
      err => {
        console.log(err);
        this.httpResponseService.showErrorMessage(err);
      });
  }

  ngOnInit(): void {
    this.rentalPointForm = new FormGroup({
      name: new FormControl('', Validators.required),
      fullAddress: new FormControl('', Validators.required)
    });
    console.log(this.rentalPointForm);
    this.rentalPointService.getRentalPointLocation(this.id).subscribe(
      data => {
        console.log(data);
        this.currentPoint = data;
        this.fullAddress = `${data.address}, ${data.city}, ${data.country}`;
        this.rentalName = data.name;
        this.lat = data.lat;
        this.lng = data.lng;
        this.initMap();
        this.rentalPointForm.patchValue({
          fullAddress: this.fullAddress
        });
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      });
  }
}
