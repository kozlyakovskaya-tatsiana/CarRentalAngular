import {Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {EditUser} from '../../../../shared/utils/User/EditUser';
import {EdituserByAdminComponent} from '../edituser-by-admin/edituser-by-admin.component';

@Component({
  selector: 'app-edit-user-modal',
  templateUrl: './edit-user-modal.component.html',
  styleUrls: ['./edit-user-modal.component.css']
})
export class EditUserModalComponent {

  constructor() { }

  @Input() user: EditUser;

  @Output() edit: EventEmitter<EditUser> = new EventEmitter<EditUser>();

  @ViewChild(EdituserByAdminComponent, {static: false})
  public editUserByAdmin: EdituserByAdminComponent;

  onEdit(user: EditUser): void{
    this.edit.emit(user);
  }

  showModal(): void{
    document.getElementById('editUserModalBtn').click();
  }

  closeModal(): void{
    document.getElementById('editUserModalCloseBtn').click();
  }
}
