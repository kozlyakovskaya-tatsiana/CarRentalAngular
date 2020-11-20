import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {LoginRequest} from '../../../shared/utils/Authorize/LoginRequest';
import {AuthorizeService} from '../../../shared/services/authorize.service';
import swal from 'sweetalert2';
import {Router} from '@angular/router';
import {UserInfoService} from '../../../shared/services/user-info.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  constructor(private authorizeService: AuthorizeService,
              private userManagementService: UserInfoService,
              private router: Router) {
    this.loginModel = new LoginRequest();
  }

  loginModel: LoginRequest;

  loginForm: FormGroup;

  isLoading: boolean;

  onSubmit(): void{
    this.isLoading = true;
    this.authorizeService.login(this.loginModel).subscribe(
      data => {
        localStorage.setItem('access_token', data.accessToken);
        localStorage.setItem('refresh_token', data.refreshToken);
        localStorage.setItem('user_id', data.userId);
        localStorage.setItem('user_email', data.userEmail);
        localStorage.setItem('user_role', data.userRole);
        if (data.userRole === 'user'){
          this.router.navigate(['']);
        }
        else if (data.userRole === 'admin'){
          this.router.navigate(['adminpage']);
        }
        else{
          this.router.navigate(['carmanagement']);
        }
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


