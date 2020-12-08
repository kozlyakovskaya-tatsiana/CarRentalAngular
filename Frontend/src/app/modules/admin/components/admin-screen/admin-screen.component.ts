import {Component, OnInit, ViewChild} from '@angular/core';
import {UserReadInfo} from '../../../../shared/utils/User/UserReadInfo';
import {AdminService} from '../../services/admin.service';
import swal from 'sweetalert2';
import {UsersTableComponent} from '../users-table/users-table.component';
import {UserInfoService} from '../../../../shared/services/user-info.service';
import {HttpResponseService} from '../../../../shared/services/http-response.service';
import {UserInfoModalComponent} from '../user-info-modal/user-info-modal.component';
import {EditUser} from '../../../../shared/utils/User/EditUser';
import {SwalService} from '../../../../shared/services/swal.service';
import {Location} from '@angular/common'
import {EditUserModalComponent} from '../edit-user-modal/edit-user-modal.component';

@Component({
  selector: 'app-admin-screen',
  templateUrl: './admin-screen.component.html',
  styleUrls: ['./admin-screen.component.css']
})
export class AdminScreenComponent implements OnInit {

  constructor(private adminService: AdminService,
              private userInfoService: UserInfoService,
              private httpResponseService: HttpResponseService,
              private swalService: SwalService,
              private location: Location) {}

  @ViewChild(UsersTableComponent, {static: false})
  private userTable: UsersTableComponent;

  @ViewChild(UserInfoModalComponent, {static: false})
  private userInfoModal: UserInfoModalComponent;

  @ViewChild(EditUserModalComponent, {static: false})
  private editUserModal: EditUserModalComponent;

  users: UserReadInfo[];

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

  getUserForEdit(id: string): void{
    this.editUserModal.showModal();
    this.userInfoService.getUser(id).subscribe(
      data => {
        console.log(data);
        this.editUserModal.editUserByAdmin.editUser = data;
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
    this.editUserModal.showModal();
  }

  onEditUser(user: EditUser): void{
    this.adminService.updateUser(user).subscribe(
      data => {
        this.swalService.showSuccessMessage('Updating is successful')
          .then(val => {
            this.editUserModal.closeModal();
          });
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }

  onGetInfo(id: string): void{
    this.userInfoService.getUser(id).subscribe(data => {
        this.userInfoModal.user = data;
        this.userInfoModal.showModal();
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }

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
