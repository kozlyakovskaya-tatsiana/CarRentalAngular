import { Component, OnInit } from '@angular/core';
import {AdminService} from '../../services/admin.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {RoleService} from '../../services/role.service';
import swal from 'sweetalert2';
import {UserCreate} from '../../../../shared/utils/User/UserCreate';
import {Location} from '@angular/common';

@Component({
  selector: 'app-createuser',
  templateUrl: './createuser.component.html',
  styleUrls: ['./createuser.component.css']
})
export class CreateuserComponent implements OnInit {

  constructor(private adminService: AdminService,
              private roleService: RoleService,
              private location: Location) {
    this.userCreate = new UserCreate();
    this.userCreate.role = 'user';
  }

  createForm: FormGroup;
  roles: string;
  userCreate: UserCreate;
  isLoading: boolean;

  onSubmit(): void{
    console.log(this.userCreate);
    this.isLoading = true;
    this.adminService.createUser(this.userCreate).subscribe(
      data => {
        console.log(data);
        swal.fire({
          title: 'Creating is successful.',
          icon: 'success'
        }).then(val => {
          this.location.back();
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
        swal.fire(
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
    this.createForm = new FormGroup({
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
      passportId: new FormControl(),
      email: new FormControl('', [
        Validators.required,
        Validators.email
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(5)
      ]),
      role: new FormControl('', Validators.required)
    });

    this.roleService.getRoles().subscribe(data => {
        this.roles = data;
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
            title: 'Error while loading roles',
            icon: 'error',
            text: errorMessage
          });
      });
  }

}
