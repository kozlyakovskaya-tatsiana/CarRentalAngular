import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {UserInfoService} from '../../../shared/services/user-info.service';
import {UserBase} from '../../../shared/utils/User/UserBase';
import swal from 'sweetalert';
import {ActivatedRoute} from '@angular/router';
import {UserService} from '../../../shared/services/user.service';
import {RoleService} from '../../admin/services/role.service';

@Component({
  selector: 'app-edituser',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  constructor(private userInfoService: UserInfoService,
              private userService: UserService,
              private activateRoute: ActivatedRoute) {
    this.userBaseInfo = new UserBase();
    this.userEditId = activateRoute.snapshot.params.id;
  }
  userBaseInfo: UserBase;
  editForm: FormGroup;
  isLoading: boolean;
  userEditId: string;

  onSubmit(): void{
    console.log(this.userBaseInfo);
    this.isLoading = true;
    this.userService.updateUserBaseInfo(this.userBaseInfo).subscribe(
      data => {
        console.log(data);
        swal({
          title: 'Updating is successful.',
          icon: 'success'
        });
      },
      err => {
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

        this.isLoading = false;
      },
      () => {
        this.isLoading = false;
      }
    );
  }

  ngOnInit(): void {
    this.editForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(20)
      ]),
      surname: new FormControl('', [
        Validators.required,
        Validators.maxLength(20)
      ]),
      dateOfBirth: new FormControl(undefined, Validators.required),
      telephoneNumber: new FormControl('', [
        Validators.required,
        Validators.pattern(/\(?\d{3}\)?-? *\d{3}-? *-?\d{4}/)
      ]),
      passportNumber: new FormControl(),
      passportId: new FormControl()
    });

    this.userInfoService.getUser(this.userEditId).subscribe(
      data => {
        this.userBaseInfo = data;
        this.userBaseInfo.dateOfBirth = new Date(data.dateOfBirth).toISOString().split('T')[0];
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
