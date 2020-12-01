import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoginRequest} from '../../../shared/utils/Authorize/LoginRequest';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

  constructor() {
    this.loginModel = new LoginRequest();
  }

  loginModel: LoginRequest;

  loginForm: FormGroup;

  @Output() Submit: EventEmitter<LoginRequest> = new EventEmitter<LoginRequest>();

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

  onSubmit(): void {
    this.Submit.emit(this.loginModel);
  }
}
