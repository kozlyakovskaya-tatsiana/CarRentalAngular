import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import {AuthorizeService} from '../services/authorize.service';
import {Location} from '@angular/common';
import swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeGuard implements CanActivate {
  constructor(private authService: AuthorizeService,
              private location: Location) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (this.authService.isAuthorized) {
      return true;
    }
    else {
      swal.fire({
        title: 'Access denied!',
        icon: 'info'
      }).then(value =>
        this.location.back()
      );
      return false;
    }
  }
}
