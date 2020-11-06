import { Component, OnInit } from '@angular/core';
import {CarService} from '../../../shared/services/car.service';
import {ActivatedRoute} from '@angular/router';
import swal from 'sweetalert';
import {CarToReadWithImage} from '../../../shared/utils/Car/CarToReadWithImage';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-car-info',
  templateUrl: './car-info.component.html',
  styleUrls: ['./car-info.component.css']
})
export class CarInfoComponent implements OnInit {

  constructor(private carService: CarService,
              private activatedRoute: ActivatedRoute) {
    this.id = activatedRoute.snapshot.params.id;
  }

  car: CarToReadWithImage;
  id: string;

  ngOnInit(): void {
    this.carService.getCar(this.id).pipe(
      map(c => {
        const car = c as CarToReadWithImage;
        car.imageNames = car.imageNames.map(name => this.carService.backendUrlForImages + name);
        console.log(car.imageNames);
        return car;
      })).subscribe(data => {
      this.car = data;
      console.log(data.imageNames);
      },
      err => {
        console.log(err);
        let errorMessage: string;
        if (err.error instanceof ProgressEvent){
          errorMessage = 'HTTP Failure to get resource';
        }
        else if (err.error?.title){
          errorMessage = err.error.title;
        }
        else {
          errorMessage = err.message;
        }
        swal(
          {
            title: 'Error',
            icon: 'error',
            text: errorMessage
          });
      }
    );
  }

}
