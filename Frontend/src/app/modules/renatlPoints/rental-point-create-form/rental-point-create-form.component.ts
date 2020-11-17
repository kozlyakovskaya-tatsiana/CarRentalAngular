import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {RentalPointService} from '../../../shared/services/rental-point.service';

@Component({
  selector: 'app-rental-point-create-form',
  templateUrl: './rental-point-create-form.component.html',
  styleUrls: ['./rental-point-create-form.component.css']
})
export class RentalPointCreateFormComponent implements OnInit {

  constructor(private rentalService: RentalPointService) {
    this.countries = new Array<string>();
  }

  createPointForm: FormGroup;
  countries: Array<string>;

  onSubmit(): void{
    alert('submit');
  }

  ngOnInit(): void {
    this.rentalService.getContries().subscribe(
      data => {
        this.countries = data;
      },
       err => {
        console.log(err);
       }
    );
    this.rentalService.getCitiesOfCountry().subscribe(
      data => {
        console.log(data);
      },
      err => {
        console.log(err);
      }
    );
    this.createPointForm = new FormGroup({
      country: new FormControl('', Validators.required),
      city: new FormControl('', Validators.required),
      address: new FormControl('', Validators.required)
    });
  }

}
