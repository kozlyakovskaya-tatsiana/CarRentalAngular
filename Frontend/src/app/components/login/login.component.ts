import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {LoginModel} from '../../utils/LoginModel';
import {AuthorizeService} from '../../services/authorize.service';
import swal from 'sweetalert';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  constructor(private authorizeService: AuthorizeService) {
    this.loginModel = new LoginModel();
  }

  loginModel: LoginModel;

  loginForm: FormGroup;

  isLoading: boolean;

  onSubmit(): void{
    this.isLoading = true;
    this.authorizeService.login(this.loginModel).subscribe(
      data => {
        this.authorizeService.userEmail = data.userEmail;
        this.authorizeService.userRole = data.userRole;
        localStorage.setItem('access_token', data.accessToken);
        localStorage.setItem('refresh_token', data.refreshToken);
      },
      err => {
        console.log(err);
        let errorMessage: string;
        if (err.error instanceof ProgressEvent){
          errorMessage = 'HTTP Failure to get resource';
        }
        if (err?.error?.errors?.Email[0]) {
          errorMessage = err.error.errors.Email[0];
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
    this.loginForm = new FormGroup({
      email : new FormControl('', [
        Validators.required,
        Validators.email
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(5)
      ])
    });
  }
}


