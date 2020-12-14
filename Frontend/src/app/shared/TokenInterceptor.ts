import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {AuthorizeService} from './services/authorize.service';
import {BehaviorSubject, Observable, of, throwError} from 'rxjs';
import {catchError, switchMap} from 'rxjs/operators';
import {environment} from '../../environments/environment';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(public authService: AuthorizeService) {
  }
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
      if (error instanceof HttpErrorResponse && error.status === 401 && this.authService.isAuthorized) {
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
      return this.authService.doRefreshToken().pipe(
        switchMap((token: any) => {
          this.refreshTokenSubject.next(token.accessToken);
          console.log('switch map');
          console.log(token);
          console.log(request);
          return next.handle(this.addToken(request, token.accessToken));
        }),
        catchError(err => {
          if (err.status === 401){
            console.log('token is expired');
            this.authService.logout();
          }
          console.log('catch err while refresh');
          console.log(err);
          return of(err);
        })
      );
      console.log('finish');
  }
}
