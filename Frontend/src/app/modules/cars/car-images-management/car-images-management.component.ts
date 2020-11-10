import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CarService} from '../../../shared/services/car.service';
import {DocumentDto} from '../../../shared/utils/Document/DocumentDto';
import {map} from 'rxjs/operators';
import swal from 'sweetalert';
import {DocumentService} from '../../../shared/services/document.service';

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
  }

  id: string;
  images: DocumentDto[];

  deleteImage(index: number): void {
    console.log(index);
    swal({
      title: 'Do you really want to delete this image?',
      icon: 'warning',
      buttons: ['No', 'Yes']
    }).then(yes => {
      const imgId = this.images[index].id;
      this.documentService.deleteDocument(imgId).subscribe(data => {
          swal({
            title: 'Deleting is successful',
            icon: 'success'
          });
          window.location.reload();
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
    });
  }

  ngOnInit(): void {
    console.log(this.id);
    this.carService.getCarsImages(this.id).pipe(
      map(docs => {
        docs.forEach(doc => doc.name = this.carService.backendUrlForImages + doc.name);
        return docs;
      })
    ).subscribe(data => {
      this.images = data;
    }, err => {
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
