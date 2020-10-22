import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {UserManagementService} from '../../services/user-management.service';
import {AuthorizeService} from '../../services/authorize.service';
import {UserBaseInfo} from '../../utils/UserBaseInfo';

@Component({
  selector: 'app-edituser',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  constructor(private userManagementService: UserManagementService,
              public authorizeService: AuthorizeService) {
    this.userBaseInfo = new UserBaseInfo();
  }
  userBaseInfo: UserBaseInfo;
  editForm: FormGroup;
  isLoading: boolean;

  onSubmit(): void{

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
      email: new FormControl('', [
        Validators.required,
        Validators.email
      ]),
      passportNumber: new FormControl(),
      passportId: new FormControl()
    });

    this.userManagementService.getUser(this.authorizeService.userEmail).subscribe(
      data => {
        this.userBaseInfo = data;
        this.userBaseInfo.dateOfBirth = new Date(data.dateOfBirth).toISOString().split('T')[0];
        console.log(data);
      },
      error => {
        console.log(error);
      }
    );
  }

}
