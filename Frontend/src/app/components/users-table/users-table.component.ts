import {Component, Input, OnInit} from '@angular/core';
import {UserReadInfo} from '../../utils/UserReadInfo';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.css']
})
export class UsersTableComponent implements OnInit {

  constructor() { }

  @Input() users: UserReadInfo[];

  ngOnInit(): void {
  }

}
