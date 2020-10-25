import { Component, OnInit } from '@angular/core';
import {UserReadInfo} from '../../utils/UserReadInfo';
import {AdminService} from '../../services/admin.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-admin-screen',
  templateUrl: './admin-screen.component.html',
  styleUrls: ['./admin-screen.component.css']
})
export class AdminScreenComponent implements OnInit {

  constructor(private adminService: AdminService) {}

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
        swal(
          {
            title: 'Error',
            icon: 'error',
            text: errorMessage
          });
      }
    );
  }
}
