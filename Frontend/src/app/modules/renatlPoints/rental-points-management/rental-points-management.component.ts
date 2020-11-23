import { Component, OnInit } from '@angular/core';
import {RentalPointService} from '../../../shared/services/rental-point.service';
import {RentalPointTableInfo} from '../../../shared/utils/rentalPoint/RentalPointTableInfo';
import {HttpResponseService} from '../../../shared/services/http-response.service';
import {RentalPointLocationInfo} from '../../../shared/utils/rentalPoint/RentalPointLocationInfo';
import {SwalService} from '../../../shared/services/swal.service';

@Component({
  selector: 'app-rental-points-management',
  templateUrl: './rental-points-management.component.html',
  styleUrls: ['./rental-points-management.component.css']
})
export class RentalPointsManagementComponent implements OnInit {

  constructor(private rentalPointService: RentalPointService,
              private httResponseService: HttpResponseService,
              private swalService: SwalService) {}

  pointsForTable: Array<RentalPointTableInfo>;

  pointsLocations: Array<RentalPointLocationInfo>;

  onDelete(id: string): void{
    console.log(id);
    this.swalService.showBeforeDeleteMessage('Do you really want to delete the point?')
      .then(res => {
        if (res.isConfirmed){
          this.rentalPointService.deleteRentalPoint(id).subscribe(
            data => {
              this.swalService.showSuccessMessage('Deleting is successful').then(
                val => {
                  window.location.reload();
                }
              );
            },
            err => {
              this.httResponseService.showErrorMessage(err);
            }
          );
        }
      });
  }

  ngOnInit(): void {
    this.rentalPointService.getRentalPointLocations().subscribe(
      data => {
        this.pointsLocations = data;
      },
      err => {
        this.httResponseService.showErrorMessage(err);
      }
    );

    this.rentalPointService.getRentalPointTableInfo().subscribe(
      data => {
        this.pointsForTable = data;
      },
      err => {
        this.httResponseService.showErrorMessage(err);
      }
    );
  }

}
