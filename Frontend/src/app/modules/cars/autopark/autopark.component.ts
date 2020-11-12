import { Component, OnInit } from '@angular/core';
import {CarForSmallCard} from '../../../shared/utils/Car/CarForSmallCard';
import {CarService} from '../../../shared/services/car.service';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-autopark',
  templateUrl: './autopark.component.html',
  styleUrls: ['./autopark.component.css']
})
export class AutoparkComponent implements OnInit {

  constructor(private carService: CarService) {
  }

  cars: CarForSmallCard[];

  ngOnInit(): void {
    this.carService.getCarsForSmallCards().pipe(
      map(cars => {
        cars.forEach(car => car.imageName = this.carService.backendUrlForImages + car.imageName);
        return cars;
      })
    )
    .subscribe(data => {
        console.log(data);
        this.cars = data;
      },
      err => {
        console.log(err);
      });
  }
}
