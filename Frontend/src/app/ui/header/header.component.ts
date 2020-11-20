import {Component, Input, OnInit} from '@angular/core';
import {AuthorizeService} from '../../shared/services/authorize.service';
import swal from 'sweetalert2';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(public authorizeService: AuthorizeService) { }

  onLogOut(): void{
    swal.fire({
      text: 'Do you really want to log out?',
      icon: 'info',
      cancelButtonText: 'No',
      confirmButtonText: 'Yes',
      showCancelButton: true
    }).then(res => {
        if (res.isConfirmed) {
          this.authorizeService.logout();
        }
    });
  }

  ngOnInit(): void {
  }

}
