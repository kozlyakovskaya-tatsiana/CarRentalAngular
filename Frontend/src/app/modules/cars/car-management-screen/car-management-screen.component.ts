import { Component, OnInit } from '@angular/core';
import swal from 'sweetalert2';
import {CarReadTableInfo} from '../../../shared/utils/Car/CarReadTableInfo';
import {CarService} from '../../../shared/services/car.service';

@Component({
  selector: 'app-car-management-screen',
  templateUrl: './car-management-screen.component.html',
  styleUrls: ['./car-management-screen.component.css']
})
export class CarManagementScreenComponent implements OnInit {

  constructor(private carService: CarService) { }

  cars: CarReadTableInfo[];

  ngOnInit(): void {
    this.carService.getCars().subscribe(data => {
        this.cars = data;
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
        swal.fire(
          {
            title: 'Error',
            icon: 'error',
            text: errorMessage
          });
      }
    );
  }

}
