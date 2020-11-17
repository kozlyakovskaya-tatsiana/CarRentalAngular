import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ICountry} from '../utils/rentalPoint/ICountry';

@Injectable({
  providedIn: 'root'
})
export class RentalPointService {

  constructor(private http: HttpClient) {
  }

  private apiForGettingCountries = 'https://restcountries.eu/rest/v2/all?fields=name';
  private apiForGettingCitiesOfCountry = 'https://countries-cities.p.rapidapi.com/location/country/US/city/list';
  headers = new HttpHeaders({
    'x-rapidapi-key': '7472d85ff7mshb47eaab269a176ep1887d3jsnb800542c2813',
    'x-rapidapi-host': 'countries-cities.p.rapidapi.com'
  });

  public getContries(): Observable<string[]>{
    return this.http.get(this.apiForGettingCountries).pipe(
      map(data => {
        const countries = data as Array<ICountry>;
        return countries.map(value => value.name);
      }));
  }

  public getCitiesOfCountry(): Observable<any>{
    return this.http.get(this.apiForGettingCitiesOfCountry, { headers: this.headers});
  }

}
