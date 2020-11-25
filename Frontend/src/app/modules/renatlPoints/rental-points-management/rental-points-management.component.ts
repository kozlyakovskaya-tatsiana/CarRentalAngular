import {Component, OnInit, ViewChild} from '@angular/core';
import {RentalPointService} from '../../../shared/services/rental-point.service';
import {RentalPointTableInfo} from '../../../shared/utils/rentalPoint/RentalPointTableInfo';
import {HttpResponseService} from '../../../shared/services/http-response.service';
import {RentalPointLocationInfo} from '../../../shared/utils/rentalPoint/RentalPointLocationInfo';
import {SwalService} from '../../../shared/services/swal.service';
import {RentalInfoComponent} from '../rental-info/rental-info.component';
import {Router} from '@angular/router';


@Component({
  selector: 'app-rental-points-management',
  templateUrl: './rental-points-management.component.html',
  styleUrls: ['./rental-points-management.component.css']
})
export class RentalPointsManagementComponent implements OnInit {

  constructor(private rentalPointService: RentalPointService,
              private httResponseService: HttpResponseService,
              private swalService: SwalService,
              private router: Router) {}

  pointsForTable: Array<RentalPointTableInfo>;

  pointsLocations: Array<RentalPointLocationInfo>;

  @ViewChild(RentalInfoComponent, {static: false})
  private infoModalComponent: RentalInfoComponent;

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

  onGetInfo(id: string): void{
    const location: RentalPointLocationInfo = this.pointsLocations.filter(p => p.id === id)[0];
    this.infoModalComponent.showModal();
    this.infoModalComponent.modalTitle = this.pointsForTable.filter(p => p.id === id)[0].name;
    this.infoModalComponent.location = location;
    this.infoModalComponent.centerLat = location.lat;
    this.infoModalComponent.centerLng = location.lng;
    this.infoModalComponent.address = `${location.country}, ${location.city}, ${location.address}`;
  }

  onGetInfoClickBtn(id: string): void{
    this.infoModalComponent.closeModal();
    this.router.navigate(['', 'autopark'], {queryParams: {pointid: id}});
  }

  onEdit(id: string): void{
    this.router.navigate(['', 'editrentalpoint', id]);
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
