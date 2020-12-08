import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {UserReadInfo} from '../../../../shared/utils/User/UserReadInfo';
import {AdminService} from '../../services/admin.service';
import swal from 'sweetalert2';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.css']
})
export class UsersTableComponent{

  constructor(private adminService: AdminService) { }

  @Input() users: UserReadInfo[];

  @Output() deleteUser: EventEmitter<string> = new EventEmitter<string>();
  @Output() getInfo: EventEmitter<string> = new EventEmitter<string>();
  @Output() updateUser: EventEmitter<string> = new EventEmitter<string>();

  onDeleteUser(id: string): void{
    this.deleteUser.emit(id);
  }

  onGetInfo(id: string): void{
    this.getInfo.emit(id);
  }

  onUpdateUser(id: string): void{
    this.updateUser.emit(id);
  }
}
