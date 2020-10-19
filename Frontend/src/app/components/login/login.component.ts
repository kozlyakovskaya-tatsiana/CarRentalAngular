import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {LoginModel} from '../../utils/LoginModel';
import {AuthorizeService} from '../../services/authorize.service';

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

  OnSubmit(): void{
    this.authorizeService.login(this.loginModel).subscribe(
      data => {
        console.log(data);

        this.authorizeService.userEmail = data.userEmail;

        this.authorizeService.userRole = data.userRole;
      },
      err => {
        console.log (err.error.errors.Email[0]);
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
        Validators.minLength(6)
      ])
    });
  }
}


