import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {AuthorizeService} from './services/authorize.service';
import {BehaviorSubject, Observable, of, throwError} from 'rxjs';
import {catchError, filter, switchMap, take} from 'rxjs/operators';
import {environment} from '../../environments/environment';
import {Router} from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(public authService: AuthorizeService) {
  }
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!request.url.includes(environment.baseApi)){
      request = request.clone();
    }
    else if (this.authService.accessToken) {
      request = this.addToken(request, this.authService.accessToken);
    }
    console.log('intercept1');

    return next.handle(request).pipe(catchError(error => {
      if (error instanceof HttpErrorResponse && error.status === 401) {
        return this.handle401Error(request, next);
      } else {
        console.log('non 401 error');
        console.log(error);
        return throwError(error);
      }
    }));
  }

  private addToken(request: HttpRequest<any>, token: string): HttpRequest<any> {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      console.log('!isrefrshing ');

      return this.authService.doRefreshToken().pipe(
        switchMap((token: any) => {
          this.isRefreshing = false;
          this.refreshTokenSubject.next(token.accessToken);
          console.log('switch map');
          return next.handle(this.addToken(request, token.accessToken));
        }),
        catchError(err => {
          console.log('catch err while refresh');
          console.log(err);
          this.authService.logout();
          return of(err);
        })
      );
    } else {
      console.log('refreshing');
      return this.refreshTokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(jwt => {
          return next.handle(this.addToken(request, jwt));
        }));
    }
  }
}
