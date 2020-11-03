import { Component, OnInit } from '@angular/core';
import {CarToRead} from '../../../shared/utils/Car/CarToRead';
import {CarService} from '../../../shared/services/car.service';
import {ActivatedRoute} from '@angular/router';
import swal from 'sweetalert';

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

  car: CarToRead;
  id: string;

  ngOnInit(): void {
    this.carService.getCar(this.id).subscribe(data => {
      this.car = data;
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
