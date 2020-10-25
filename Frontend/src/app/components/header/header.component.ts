import {Component, Input, OnInit} from '@angular/core';
import {AuthorizeService} from '../../services/authorize.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(public authorizeService: AuthorizeService) { }

  onLogOut(): void{
    swal({
      text: 'Do you really want to log out?',
      icon: 'info',
      buttons: ['No', 'Yes'],
      dangerMode: true
    }).then(logout => {
        if (logout) {
          this.authorizeService.logout();
        }
    });
  }

  ngOnInit(): void {
  }

}
