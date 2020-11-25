import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {RentalPointLocationInfo} from '../../../shared/utils/rentalPoint/RentalPointLocationInfo';

@Component({
  selector: 'app-map-markers',
  templateUrl: './map-markers.component.html',
  styleUrls: ['./map-markers.component.css']
})
export class MapMarkersComponent implements OnChanges {

  constructor() { }

  @Input() centerLat: number = environment.defaultLat;
  @Input() centerLng: number = environment.defaultLng;
  @Input() iconUrl: string;
  @Input() locations: Array<RentalPointLocationInfo> = new Array<RentalPointLocationInfo>();


  public initMap(): void{
    const map = new google.maps.Map(
      document.getElementById('map'), {
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
        scaledSize: new google.maps.Size(30, 30), // scaled size
        origin: new google.maps.Point(0, 0), // origin
        anchor: new google.maps.Point(0, 0) // anchor
      };
    }

    this.locations?.forEach((loc) => {
      const marker = new google.maps.Marker({
        map,
        position: new google.maps.LatLng(loc.lat, loc.lng),
        animation: google.maps.Animation.BOUNCE,
        title: `${loc.country} ${loc.city} ${loc.address}`
      });
      marker.addListener('click', () => {
        map.setZoom(map.getZoom() + 8);
        map.setCenter(marker.getPosition() as google.maps.LatLng);
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
}
