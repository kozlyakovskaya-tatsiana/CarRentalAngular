import { Injectable } from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router} from '@angular/router';
import { Observable } from 'rxjs';
import {AuthorizeService} from '../services/authorize.service';
import swal from 'sweetalert';
import {Location} from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AdminAccessGuard implements CanActivate {
  constructor(private authService: AuthorizeService,
              private location: Location) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (!this.authService.isAdmin){
      swal({
        title: 'Access only for admins!',
        icon: 'info'
      }).then(value =>
        this.location.back()
      );
      return false;
    }
    return true;
  }
}
