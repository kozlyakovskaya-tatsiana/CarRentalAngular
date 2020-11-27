import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-login-modal',
  templateUrl: './login-modal.component.html',
  styleUrls: ['./login-modal.component.css']
})
export class LoginModalComponent implements OnInit {

  constructor() { }

  showModal(): void{
    document.getElementById('loginModalBtn').click();
  }

  closeModal(): void{
    document.getElementById('closeLoginModalBtn').click();
  }

  ngOnInit(): void {
  }

}
