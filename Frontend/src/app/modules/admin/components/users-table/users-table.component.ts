import {Component, Input, OnInit, Output} from '@angular/core';
import {UserReadInfo} from '../../../../shared/utils/User/UserReadInfo';
import {AdminService} from '../../services/admin.service';
import swal from 'sweetalert2';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.css']
})
export class UsersTableComponent{

  constructor(private adminService: AdminService) { }

  @Input() users: UserReadInfo[];

  onDelete(id: string): void{
    swal.fire({
      text: 'Do you really want to delete this user?',
      icon: 'info',
      cancelButtonText: 'No',
      confirmButtonText: 'Yes',
      showCancelButton: true
    }).then(res => {
      if (res.isConfirmed) {
        this.adminService.deleteUser(id).subscribe(
          data => {
            console.log(data);
            swal.fire({
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
            swal.fire(
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
