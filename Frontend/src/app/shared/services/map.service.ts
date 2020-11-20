import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MapService {

  constructor(private http: HttpClient) { }

  private apiKey = 'AIzaSyDLUeOZwMQ6QivFjbHBuauXjQAr6fYGpIQ';

  public resetBearerHeader = true;

  public geocodeReverse(lat: number, lng: number): Observable<any>{
    return this.http.get(`https://maps.googleapis.com/maps/api/geocode/json?latlng=${lat},${lng}&key=${this.apiKey}
    &result_type=street_address`);
  }

}
