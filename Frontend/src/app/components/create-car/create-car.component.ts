import { Component, OnInit } from '@angular/core';
import {CarService} from '../../services/car.service';

@Component({
  selector: 'app-create-car',
  templateUrl: './create-car.component.html',
  styleUrls: ['./create-car.component.css']
})
export class CreateCarComponent implements OnInit {

  constructor(private carService: CarService) {}

  ngOnInit(): void {
    this.carService.getTransmissionsTypes().subscribe(data => {
      console.log(data);
    },
      err => {
      console.log(err);
      });
  }

}
