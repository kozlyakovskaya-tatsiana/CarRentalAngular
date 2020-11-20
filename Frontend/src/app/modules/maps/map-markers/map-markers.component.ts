import {Component, Input, OnInit} from '@angular/core';
import {LocationCoords} from '../../../shared/utils/rentalPoint/LocationCoords';

@Component({
  selector: 'app-map-markers',
  templateUrl: './map-markers.component.html',
  styleUrls: ['./map-markers.component.css']
})
export class MapMarkersComponent implements OnInit {

  constructor() { }

  @Input() centerLat: number;
  @Input() centerLng: number;

  @Input() coords: Array<LocationCoords>;

  private initMap(): void{
    const map = new google.maps.Map(
      document.getElementById('map') as HTMLElement,
      {
        center: {lat: this.centerLat, lng: this.centerLng},
        zoom: 6,
        mapTypeId: 'roadmap',
      }
    );

    const carIcon = {
      url: 'assets/car-icon.png',
      scaledSize: new google.maps.Size(30, 30), // scaled size
      origin: new google.maps.Point(0, 0), // origin
      anchor: new google.maps.Point(0, 0) // anchor
    };

    this.coords?.forEach((coords) => {
      const markerCar = new google.maps.Marker({
        map,
        position: new google.maps.LatLng(coords.lat, coords.lng),
        icon: carIcon,
        animation: google.maps.Animation.BOUNCE
      });
    });

    google.maps.event.addListener(map, 'click', (event) => {

      map.panTo(event.latLng);

      /*map.setZoom(map.getZoom() + 1);
      map.setCenter(marker.getPosition() as google.maps.LatLng);*/
    });
  }

  ngOnInit(): void {
  }

}
