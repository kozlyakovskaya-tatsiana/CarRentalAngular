import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CarService} from '../../../shared/services/car.service';
import {Location} from '@angular/common';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import swal from 'sweetalert';
import {CarTechInfo} from '../../../shared/utils/Car/CarTechInfo';
import {CarReadWithImage} from '../../../shared/utils/Car/CarReadWithImage';

@Component({
  selector: 'app-car-edit',
  templateUrl: './car-edit.component.html',
  styleUrls: ['./car-edit.component.css']
})
export class CarEditComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute,
              private carService: CarService,
              private location: Location) {
    this.id = activatedRoute.snapshot.params.id;
    this.car = new CarReadWithImage();
    this.carToUpdate = new CarTechInfo();
  }

  id: string;
  car: CarReadWithImage;
  carToUpdate: CarTechInfo;
  carcases: string[];
  transmissionTypes: string[];
  fuelTypes: string[];
  statusTypes: string[];
  carImgSrc: string;

  CarForm: FormGroup;
  isLoading: boolean;

  onSubmit(): void{
   Object.assign(this.carToUpdate, this.car);
   console.log(this.carToUpdate);
   this.isLoading = true;
   this.carService.updateCarTechInfo(this.carToUpdate).subscribe(data => {
        swal({
          title: 'Updating is successful.',
          icon: 'success'
        }).then(val => {
          this.location.back();
        });
      },
      err => {
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

        this.isLoading = false;
      },
      () => {
        this.isLoading = false;
      }
    );
  }

  ngOnInit(): void {
    this.carService.getCar(this.id).subscribe(data => {
        console.log(data);
        this.car = data;
        this.carImgSrc = this.carService.backendUrlForImages + data.imageNames[0];
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

    this.carService.getCarcases().subscribe(data => {
        console.log(data);
        this.carcases = data;
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
            title: 'Error while loading carcases',
            icon: 'error',
            text: errorMessage
          });
      });

    this.carService.getTransmissionsTypes().subscribe(data => {
        console.log(data);
        this.transmissionTypes = data;
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
            title: 'Error while loading transmission types',
            icon: 'error',
            text: errorMessage
          });
      });

    this.carService.getFuelTypes().subscribe(data => {
        console.log(data);
        this.fuelTypes = data;
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
            title: 'Error while loading fuel types',
            icon: 'error',
            text: errorMessage
          });
      });

    this.carService.getStatusTypes().subscribe(data => {
        console.log(data);
        this.statusTypes = data;
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
            title: 'Error while loading status types',
            icon: 'error',
            text: errorMessage
          });
      });

    this.CarForm = new FormGroup({
      mark: new FormControl('', Validators.required),
      model: new FormControl('', Validators.required),
      carcase: new FormControl(this.car.carcase, Validators.required),
      releaseYear: new FormControl('', Validators.required),
      transmission: new FormControl(this.car.transmission, Validators.required),
      enginePower: new FormControl('', Validators.required),
      fuelConsumption: new FormControl('', Validators.required),
      tankVolume: new FormControl('', Validators.required),
      fuelType: new FormControl(this.car.fuelType, Validators.required),
      trunkVolume: new FormControl('', Validators.required),
      status: new FormControl('', Validators.required)
    });
  }
}
