import {Component, Input, OnInit, Output} from '@angular/core';
import {UserReadInfo} from '../../utils/User/UserReadInfo';
import {AdminService} from '../../services/admin.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.css']
})
export class UsersTableComponent{

  constructor(private adminService: AdminService) { }

  @Input() users: UserReadInfo[];

  onDelete(id: string, index: number): void{
    swal({
      text: 'Do you really want to delete this user?',
      icon: 'info',
      buttons: ['No', 'Yes'],
      dangerMode: true
    }).then(yes => {
      if (yes) {
        this.adminService.deleteUser(id).subscribe(
          data => {
            console.log(data);
            this.users.splice(index, 1);
            swal({
              text: 'Deleting is successful',
              icon: 'success'
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
