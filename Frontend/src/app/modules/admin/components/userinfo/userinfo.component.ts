import {Component, Input, OnInit} from '@angular/core';
import {UserReadInfo} from '../../../../shared/utils/User/UserReadInfo';
import {ActivatedRoute} from '@angular/router';
import swal from 'sweetalert2';
import {UserInfoService} from '../../../../shared/services/user-info.service';

@Component({
  selector: 'app-userinfo',
  templateUrl: './userinfo.component.html',
  styleUrls: ['./userinfo.component.css']
})
export class UserinfoComponent implements OnInit {

  constructor(private activateRoute: ActivatedRoute,
              private userInfoService: UserInfoService){
    this.id = activateRoute.snapshot.params.id;
  }

  id: string;
  user: UserReadInfo;

  ngOnInit(): void {
    this.userInfoService.getUser(this.id).subscribe(data => {
      this.user = data;
      console.log(data);
      },
      err => {
        console.log(err);
        let errorMessage: string;
        if (err.error instanceof ProgressEvent){
          errorMessage = 'HTTP Failure to get resource';
        }
        else if (err.error?.title){
          errorMessage = err.error.title;
        }
        else {
          errorMessage = err.message;
        }
        swal.fire(
          {
            title: 'Error',
            icon: 'error',
            text: errorMessage
          });
      }
    );
  }

}
