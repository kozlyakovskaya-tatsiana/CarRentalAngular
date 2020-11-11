import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CarService} from '../../../shared/services/car.service';
import {DocumentDto} from '../../../shared/utils/Document/DocumentDto';
import {map} from 'rxjs/operators';
import swal from 'sweetalert';
import {DocumentService} from '../../../shared/services/document.service';
import {CarForEditImageRead} from '../../../shared/utils/Car/CarForEditImageRead';

@Component({
  selector: 'app-car-images-management',
  templateUrl: './car-images-management.component.html',
  styleUrls: ['./car-images-management.component.css']
})
export class CarImagesManagementComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute,
              private documentService: DocumentService,
              public carService: CarService) {
    this.id = activatedRoute.snapshot.params.id;
    this.imagesToAdd = new Array<File>();
    this.imagesToAddSrcs = new Array<any>();
  }

  id: string;
  carForEditImages: CarForEditImageRead;
  imagesToAdd: Array<File>;
  imagesToAddSrcs: Array<any>;


  deleteCarImage(index: number): void {
    console.log(index);
    swal({
      title: 'Do you really want to delete this image?',
      icon: 'warning',
      buttons: ['No', 'Yes']
    }).then(yes => {
      if (yes)
      {
        const imgId = this.carForEditImages.images[index].id;
        this.documentService.deleteDocument(imgId).subscribe(data => {
          swal({
            title: 'Deleting is successful',
            icon: 'success'
          }).then(val => {
            window.location.reload();
          });
        },
          err => {
            console.log(err);
            let errorMessage: string;
            if (err.error instanceof ProgressEvent) {
              errorMessage = 'HTTP Failure to get resource';
            } else if (err.error?.title) {
              errorMessage = err.error.title;
            } else {
              errorMessage = err.message;
            }
            swal(
              {
                title: 'Error while loading fuel types',
                icon: 'error',
                text: errorMessage
              });
          });
      }
    });
  }

  deleteTempImage(index: number): void{
    this.imagesToAddSrcs.splice(index, 1);
    this.imagesToAdd.splice(index, 1);
    console.log(this.imagesToAdd);
  }

  onChangeImages(event): void {
    if (event.target.files && event.target.files.length) {
      this.imagesToAdd = this.imagesToAdd.concat(Array.from(event.target.files));
      [...event.target.files].forEach(file => {
        const fileReader = new FileReader();
        fileReader.onload = e => {
          this.imagesToAddSrcs.push(e.target.result);
        };
        fileReader.readAsDataURL(file);
      });
      console.log(this.imagesToAddSrcs);
    }
  }

  uploadImages(): void{
    const formData = new FormData();
    formData.append('carId', this.carForEditImages.carId);
    this.imagesToAdd.forEach(img => formData.append('images', img));
    this.carService.addCarImages(formData).subscribe(data => {
        console.log(data);
        swal({
          title: 'Updating is successful',
          icon: 'success'
        }).then(val => {
          window.location.reload();
        });
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
  });
  }

  ngOnInit(): void {
    console.log(this.id);
    this.carService.getCarsForEditImages(this.id).pipe(
      map(car => {
        car.images.forEach((img => img.name = this.carService.backendUrlForImages + img.name));
        return car;
      })
    ).subscribe(
      data => {
        this.carForEditImages = data;
        console.log(this.carForEditImages);
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
  }
}
