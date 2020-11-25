import { Component, OnInit } from '@angular/core';
import {CarForSmallCard} from '../../../shared/utils/Car/CarForSmallCard';
import {CarService} from '../../../shared/services/car.service';
import {catchError, first, map, switchMap} from 'rxjs/operators';
import swal from 'sweetalert2';
import {ActivatedRoute, Router} from '@angular/router';
import {Observable, of} from 'rxjs';
import {RentalPointService} from '../../../shared/services/rental-point.service';
import {HttpResponseService} from '../../../shared/services/http-response.service';
import {Guid} from 'guid-typescript';

@Component({
  selector: 'app-autopark',
  templateUrl: './autopark.component.html',
  styleUrls: ['./autopark.component.css']
})
export class AutoparkComponent implements OnInit {

  constructor(private carService: CarService,
              private rentalPointService: RentalPointService,
              private route: ActivatedRoute,
              private httpResponseService: HttpResponseService) {
    this.rentalPointId = this.route.snapshot.params.pointid ?? '';
  }

  cars: CarForSmallCard[];
  cars$: Observable<Array<CarForSmallCard>>;
  private rentalPointId: string;
  rentalPointName: string;
  rentalPointName$: Observable<string>;

  showModalForBooking(): void{
    swal.fire({
      title: 'Form to rent a car'
    });
  }

  ngOnInit(): void {
    this.cars$ = this.rentalPointService.getRentalPointCars(this.rentalPointId).pipe(
      map(cars => {
        cars?.forEach(car => {
          if (car.imageName) {
            car.imageName = this.carService.backendUrlForImages + car.imageName;
          }
        });
        return cars;
      }),
      catchError(err  => {
        this.httpResponseService.showErrorMessage(err);
        return of(err);
      })
    );
    if (this.rentalPointId){
      this.rentalPointName$ = this.rentalPointService.getRentalPointsNames(this.rentalPointId).pipe(
        first(),
        catchError(err  => {
          this.httpResponseService.showErrorMessage(err);
          return of(err);
        })
      );
    }
  }
}
