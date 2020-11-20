import {Component, OnInit, ViewChild} from '@angular/core';
import {UserReadInfo} from '../../../../shared/utils/User/UserReadInfo';
import {AdminService} from '../../services/admin.service';
import swal from 'sweetalert2';
import {UsersTableComponent} from '../users-table/users-table.component';

@Component({
  selector: 'app-admin-screen',
  templateUrl: './admin-screen.component.html',
  styleUrls: ['./admin-screen.component.css']
})
export class AdminScreenComponent implements OnInit {

  constructor(private adminService: AdminService) {}

  @ViewChild(UsersTableComponent, {static: false})
  private userTable: UsersTableComponent;

  users: UserReadInfo[];

  ngOnInit(): void {
    this.adminService.getAllUsers().subscribe(
      data => {
        this.users = data;
        this.users.forEach( u => u.dateOfBirth = new Date(u.dateOfBirth).toISOString().split('T')[0]);
        console.log(data);
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
      }
    );
  }
}
