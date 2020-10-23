import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {UserManagementService} from '../../services/user-management.service';
import {AuthorizeService} from '../../services/authorize.service';
import {UserBaseInfo} from '../../utils/UserBaseInfo';
import swal from 'sweetalert';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-edituser',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  constructor(private userManagementService: UserManagementService,
              public authorizeService: AuthorizeService,
              private activateRoute: ActivatedRoute) {
    this.userBaseInfo = new UserBaseInfo();
    this.userEditId = activateRoute.snapshot.params.id;
  }
  userBaseInfo: UserBaseInfo;
  editForm: FormGroup;
  isLoading: boolean;
  userEditId: string;

  onSubmit(): void{
    console.log(this.userBaseInfo);
    this.isLoading = true;
    this.userManagementService.updateUserBaseInfo(this.userBaseInfo).subscribe(
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

    this.userManagementService.getUser(this.userEditId).subscribe(
      data => {

        this.userBaseInfo = data;
        this.userBaseInfo.dateOfBirth = new Date(data.dateOfBirth).toISOString().split('T')[0];
        console.log(data);
      },
      err => {
        console.log(err);
        let errorMessage: string;
        if (err.error instanceof ProgressEvent){
          errorMessage = 'HTTP Failure to get resource';
        }
        else {
          errorMessage = err.error.title;
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
