import {Component, EventEmitter, Injectable, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {RentalPointLocationInfo} from '../../../shared/utils/rentalPoint/RentalPointLocationInfo';

@Component({
  selector: 'app-map-points',
  templateUrl: './map-points.component.html',
  styleUrls: ['./map-points.component.css']
})
export class MapPointsComponent implements OnChanges, OnInit {

  constructor() { }

  @Input() centerLat: number = environment.defaultLat;
  @Input() centerLng: number = environment.defaultLng;
  @Input() iconUrl: string;
  @Input() locations: Array<RentalPointLocationInfo> = new Array<RentalPointLocationInfo>();

  @Output() markerClicked: EventEmitter<RentalPointLocationInfo> = new EventEmitter<RentalPointLocationInfo>();


  private initMap(): void{
    const map = new google.maps.Map(
      document.getElementById('map-points'), {
        center: {lat: this.centerLat, lng: this.centerLng},
        zoom: 6,
        mapTypeId: 'roadmap',
      }
    );

    let icon;
    if (this.iconUrl)
    {
      icon = {
        url: this.iconUrl,
        scaledSize: new google.maps.Size(30, 35), // scaled size
        origin: new google.maps.Point(0, 0), // origin
        anchor: new google.maps.Point(0, 0) // anchor
      };
    }

    this.locations?.forEach((loc) => {
      const marker = new google.maps.Marker({
        map,
        position: new google.maps.LatLng(loc.lat, loc.lng),
        animation: google.maps.Animation.DROP,
        title: `${loc.country} ${loc.city} ${loc.address}`
      });
      marker.addListener('click', () => {
        map.setZoom(17);
        map.setCenter(marker.getPosition() as google.maps.LatLng);
        this.markerClicked.emit(loc);
      });
      if (icon){
        marker.setIcon(icon);
      }
    });

    google.maps.event.addListener(map, 'click', (event) => {
      map.panTo(event.latLng);
      map.setZoom(map.getZoom() + 1);
      map.setCenter(event.latLng);
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.initMap();
  }

  ngOnInit(): void{
    this.initMap();
  }

}
