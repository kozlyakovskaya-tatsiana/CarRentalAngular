import {Component, Input, OnInit} from '@angular/core';
import {CarReadTableInfo} from '../../../shared/utils/Car/CarReadTableInfo';
import {CarService} from '../../../shared/services/car.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-car-table',
  templateUrl: './car-table.component.html',
  styleUrls: ['./car-table.component.css']
})
export class CarTableComponent {

  constructor(private carService: CarService) { }

  @Input() cars: CarReadTableInfo[];

  onDelete(id: string): void{
    swal({
      text: 'Do you really want to delete this cars?',
      icon: 'info',
      buttons: ['No', 'Yes'],
      dangerMode: true
    }).then(yes => {
      if (yes) {
        this.carService.removeCar(id).subscribe(
          data => {
            console.log(data);
            swal({
              text: 'Deleting is successful',
              icon: 'success'
            }).then(result => {
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
    });
  }
}
