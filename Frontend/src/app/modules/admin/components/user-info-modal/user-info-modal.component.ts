import {Component, Input, OnInit} from '@angular/core';
import {UserReadInfo} from '../../../../shared/utils/User/UserReadInfo';

@Component({
  selector: 'app-user-info-modal',
  templateUrl: './user-info-modal.component.html',
  styleUrls: ['./user-info-modal.component.css']
})
export class UserInfoModalComponent{

  constructor() { }

  @Input() user: UserReadInfo;

  showModal(): void{
    document.getElementById('infoModalBtn').click();
  }

  closeModal(): void{
    document.getElementById('modalCloseBtn').click();
  }

}
