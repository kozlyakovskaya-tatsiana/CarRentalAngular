import { Component, OnInit } from '@angular/core';
import {CarService} from '../../../shared/services/car.service';
import swal from 'sweetalert';
import {CarToCreate} from '../../../shared/utils/Car/CarToCreate';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Location} from '@angular/common';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-create-car',
  templateUrl: './create-car.component.html',
  styleUrls: ['./create-car.component.css']
})
export class CreateCarComponent implements OnInit {

  constructor(private carService: CarService,
              private location: Location,
              private http: HttpClient) {
    this.carToCreate = new CarToCreate();
    this.imageSrcs = [];
    this.imageFilesArray = new Array<File>();
  }

  carToCreate: CarToCreate;
  carcases: string[];
  transmissionTypes: string[];
  fuelTypes: string[];
  createCarForm: FormGroup;
  isLoading: boolean;

  imageSrcs: any;

  imageFilesArray: Array<File>;

  onChangeImages(event): void{
    if (event.target.files && event.target.files.length) {
      this.imageFilesArray = this.imageFilesArray.concat(Array.from(event.target.files));
      this.imageFilesArray.forEach(file => {
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
      this.createCarForm.patchValue({images: null});
    }
  }

  onSubmit(): void{
    const form = document.forms[0];
    const formData = new FormData(form);
    this.imageFilesArray.forEach(imgFile => {
      formData.append('images', imgFile);
    });
    console.log(this.imageFilesArray);
    this.carService.createCar(formData).subscribe(data => {
      this.isLoading = true;
      console.log(data);
      swal({
          title: 'Creating cars is successful',
          icon: 'success'
        })
        .then(val => {
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
        swal(
          {
            title: 'Error while loading fuel types',
            icon: 'error',
            text: errorMessage
          });
      });

    this.createCarForm = new FormGroup({
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
      images: new FormControl(null, Validators.required)
    });
  }
}
