import { Component, OnInit } from '@angular/core';
import SearchBoxOptions = google.maps.places.SearchBoxOptions;

@Component({
  selector: 'app-map-searchbox',
  templateUrl: './map-searchbox.component.html',
  styleUrls: ['./map-searchbox.component.css']
})
export class MapSearchboxComponent implements OnInit {
  constructor() {
  }

  lat = 52.14052719475131;
  lng = 26.102992085204164;

  place: string;

  ngOnInit(): void {
    const map = new google.maps.Map(
      document.getElementById('map') as HTMLElement,
      {
        center: {lat: this.lat, lng: this.lng},
        zoom: 6,
        mapTypeId: 'roadmap',
      }
    );

    const defaultBounds = new google.maps.LatLngBounds(
      new google.maps.LatLng(-33.8902, 151.1759),
      new google.maps.LatLng(-33.8474, 151.2631));

    const options: SearchBoxOptions = {
      bounds: defaultBounds
    };

    const input = document.getElementById('pac-input') as HTMLInputElement;
    const searchBox = new google.maps.places.SearchBox(input, options as SearchBoxOptions);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    map.addListener('bounds_changed', () => {
      searchBox.setBounds(map.getBounds() as google.maps.LatLngBounds);
    });

    let markers: google.maps.Marker[] = [];

    searchBox.addListener('places_changed', () => {
      const places = searchBox.getPlaces();
      console.log(places);
      if (places.length === 0) {
        return;
      }
      markers.forEach((marker) => {
        marker.setMap(null);
      });
      markers = [];

      const bounds = new google.maps.LatLngBounds();
      places.forEach((place) => {
        if (!place.geometry) {
          console.log('Returned place contains no geometry');
          return;
        }
        const icon = {
          url: place.icon as string,
          size: new google.maps.Size(71, 71),
          origin: new google.maps.Point(0, 0),
          anchor: new google.maps.Point(17, 34),
          scaledSize: new google.maps.Size(25, 25),
        };

        markers.push(
          new google.maps.Marker({
            map,
            icon,
            title: place.name,
            position: place.geometry.location,
          })
        );

        if (place.geometry.viewport) {
          bounds.union(place.geometry.viewport);
        } else {
          bounds.extend(place.geometry.location);
        }
      });
      map.fitBounds(bounds);
    });
  }
}
