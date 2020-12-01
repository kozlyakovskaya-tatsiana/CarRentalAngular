import {Component} from '@angular/core';
import {LoginRequest} from '../../../shared/utils/Authorize/LoginRequest';
import {AuthorizeService} from '../../../shared/services/authorize.service';
import {Router} from '@angular/router';
import {UserInfoService} from '../../../shared/services/user-info.service';
import {HttpResponseService} from '../../../shared/services/http-response.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent{

  constructor(private authorizeService: AuthorizeService,
              private userManagementService: UserInfoService,
              private httpResponseService: HttpResponseService,
              private router: Router) {
  }

  onSubmit(loginModel: LoginRequest): void{
    this.authorizeService.login(loginModel).subscribe(
      data => {
        if (data.userRole === 'user'){
          this.router.navigate(['']);
        }
        else if (data.userRole === 'admin'){
          this.router.navigate(['adminpage']);
        }
        else{
          this.router.navigate(['carmanagement']);
        }
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }
}


