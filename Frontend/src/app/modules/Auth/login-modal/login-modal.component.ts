import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoginRequest} from '../../../shared/utils/Authorize/LoginRequest';

@Component({
  selector: 'app-login-modal',
  templateUrl: './login-modal.component.html',
  styleUrls: ['./login-modal.component.css']
})
export class LoginModalComponent implements OnInit {

  constructor() { }

  @Output() Submit: EventEmitter<LoginRequest> = new EventEmitter<LoginRequest>();

  showModal(): void{
    document.getElementById('loginModalBtn').click();
  }

  closeModal(): void{
    document.getElementById('closeLoginModalBtn').click();
  }

  onSubmit(loginModel: LoginRequest): void{
    this.Submit.emit(loginModel);
  }

  ngOnInit(): void {
  }

}
