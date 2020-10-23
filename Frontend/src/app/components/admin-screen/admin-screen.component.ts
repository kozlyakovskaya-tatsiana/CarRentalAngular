import { Component, OnInit } from '@angular/core';
import {UserManagementService} from '../../services/user-management.service';
import {UserFullInfo} from '../../utils/UserFullInfo';

@Component({
  selector: 'app-admin-screen',
  templateUrl: './admin-screen.component.html',
  styleUrls: ['./admin-screen.component.css']
})
export class AdminScreenComponent implements OnInit {

  constructor(private userService: UserManagementService) {}

  users: UserFullInfo[];

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe(
      data => {
        this.users = data;
        console.log(data);
      },
      error => {
        console.log(error);
      }
      );
  }
}
