import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {UserInfoService} from '../../../../shared/services/user-info.service';
import {ActivatedRoute} from '@angular/router';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import swal from 'sweetalert2';
import {EditUser} from '../../../../shared/utils/User/EditUser';
import {RoleService} from '../../services/role.service';
import {AdminService} from '../../services/admin.service';
import {Location} from '@angular/common';

@Component({
  selector: 'app-edituser-by-admin',
  templateUrl: './edituser-by-admin.component.html',
  styleUrls: ['./edituser-by-admin.component.css']
})
export class EdituserByAdminComponent implements OnInit {

  constructor(private userInfoService: UserInfoService,
              private adminService: AdminService,
              private roleService: RoleService,
              private activateRoute: ActivatedRoute,
              private location: Location) {
  }
  @Input() editUser: EditUser = new EditUser();
  editForm: FormGroup;
  isLoading: boolean;

  roles: string[];

  @Output() edit: EventEmitter<EditUser> = new EventEmitter<EditUser>();

  onSubmit(): void{
    this.edit.emit(this.editUser);
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
      passportId: new FormControl(),
      role: new FormControl(Validators.required)
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
