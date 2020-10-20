import { Component, OnInit } from '@angular/core';
import {RegisterModel} from '../../utils/RegisterModel';
import {FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';
import {AuthorizeService} from '../../services/authorize.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private authorizeService: AuthorizeService) {
    this.registerModel = new RegisterModel();
  }

  registerModel: RegisterModel;

  registerForm: FormGroup;

  isLoading: boolean;

  comparisonValidator(): ValidatorFn {
    return (group: FormGroup): ValidationErrors => {
      const pass1 = group.controls.password;
      const pass2 = group.controls.passwordConfirm;
      if (pass1.value !== pass2.value) {
        pass2.setErrors({notCompare: true});
      } else {
        pass2.setErrors(null);
      }
      if (!pass2.value) {
        pass2.setErrors({required: true});
      }
      return;
    };
  }

  onSubmit(): void{
    this.isLoading = true;
    this.authorizeService.register(this.registerModel).subscribe(
      data => {
        swal({
            title: 'Registration successful',
            icon: 'success'
          }
        );
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
    this.registerForm = new FormGroup({
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
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(5)
      ]),
      passwordConfirm: new FormControl()
   });
    this.registerForm.setValidators(this.comparisonValidator());
  }
}
