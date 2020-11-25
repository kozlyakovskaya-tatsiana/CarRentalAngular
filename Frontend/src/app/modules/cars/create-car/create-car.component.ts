import { Component, OnInit } from '@angular/core';
import {CarService} from '../../../shared/services/car.service';
import swal from 'sweetalert2';
import {CarToCreate} from '../../../shared/utils/Car/CarToCreate';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Location} from '@angular/common';
import {HttpClient} from '@angular/common/http';
import {RentalPointService} from '../../../shared/services/rental-point.service';
import {HttpResponseService} from '../../../shared/services/http-response.service';

@Component({
  selector: 'app-create-car',
  templateUrl: './create-car.component.html',
  styleUrls: ['./create-car.component.css']
})
export class CreateCarComponent implements OnInit {

  constructor(private carService: CarService,
              private location: Location,
              private http: HttpClient,
              private rentalPointService: RentalPointService,
              private httpResponseService: HttpResponseService) {
    this.carToCreate = new CarToCreate();
    this.imageSrcs = [];
    this.imageFilesArray = new Array<File>();
  }

  carToCreate: CarToCreate;
  carcases: string[];
  transmissionTypes: string[];
  fuelTypes: string[];
  statusTypes: string[];
  CarForm: FormGroup;
  isLoading: boolean;

  imageSrcs: any;
  imageFilesArray: Array<File>;

  rentalPoints: Array<string>;

  onChangeImages(event): void {
    if (event.target.files && event.target.files.length) {
      this.imageFilesArray = this.imageFilesArray.concat(Array.from(event.target.files));
      [...event.target.files].forEach(file => {
        const fileReader = new FileReader();
        fileReader.onload = e => {
          this.imageSrcs.push(e.target.result);
        };
        fileReader.readAsDataURL(file);
      });
      console.log(this.imageFilesArray);
      }
  }

  deleteImage(index: number): void {
        this.imageSrcs.splice(index, 1);
        this.imageFilesArray.splice(index, 1);
        console.log(this.imageFilesArray);
        if (!this.imageFilesArray.length)
        {
          this.CarForm.patchValue({images: null});
        }
  }
  onSubmit(): void{
        const form = document.forms[0];
        const formData = new FormData(form);
        this.imageFilesArray.forEach(imgFile => {
          formData.append('images', imgFile);
        });
        console.log(this.imageFilesArray);
        this.isLoading = true;
        this.carService.createCar(formData).subscribe(data => {
            swal.fire({
              title: 'Creating is successful.',
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
              swal.fire(
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
    this.carService.getCarcases().subscribe(data => {
        console.log(data);
        this.carcases = data;
        this.carToCreate.carcase = this.carcases[0];
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
            title: 'Error while loading carcases',
            icon: 'error',
            text: errorMessage
          });
    });

    this.carService.getTransmissionsTypes().subscribe(data => {
        console.log(data);
        this.transmissionTypes = data;
        this.carToCreate.transmission = this.transmissionTypes[0];
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
            title: 'Error while loading transmission types',
            icon: 'error',
            text: errorMessage
          });
      });

    this.carService.getFuelTypes().subscribe(data => {
        console.log(data);
        this.fuelTypes = data;
        this.carToCreate.fuelType = this.fuelTypes[0];
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
            title: 'Error while loading fuel types',
            icon: 'error',
            text: errorMessage
          });
      });

    this.carService.getStatusTypes().subscribe(data => {
          console.log(data);
          this.statusTypes = data;
          this.carToCreate.status = this.statusTypes[0];
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
              title: 'Error while loading status types',
              icon: 'error',
              text: errorMessage
            });
        });

    this.rentalPointService.getRentalPointsNames().subscribe(
      data => {
        this.rentalPoints = data;
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );

    this.CarForm = new FormGroup({
      mark: new FormControl('', Validators.required),
      model: new FormControl('', Validators.required),
      carcase: new FormControl(this.carToCreate.carcase, Validators.required),
      releaseYear: new FormControl('', Validators.required),
      transmission: new FormControl(this.carToCreate.transmission, Validators.required),
      enginePower: new FormControl('', Validators.required),
      fuelConsumption: new FormControl('', Validators.required),
      tankVolume: new FormControl('', Validators.required),
      fuelType: new FormControl(this.carToCreate.fuelType, Validators.required),
      trunkVolume: new FormControl('', Validators.required),
      status: new FormControl('', Validators.required),
      images: new FormControl(null, Validators.required),
      costPerDay: new FormControl('', Validators.required),
      passengersAmount: new FormControl('', Validators.required),
      doorsAmount: new FormControl('', Validators.required),
      rentalPoint: new FormControl('', Validators.required)
    });
  }
}
