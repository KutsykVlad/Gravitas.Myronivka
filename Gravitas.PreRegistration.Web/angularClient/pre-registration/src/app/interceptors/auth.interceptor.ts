import { AuthService } from '../services/auth.service';
import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpEvent,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (
      request.url.indexOf('/token') >= 0 ||
      request.url.indexOf('/api/account/register') >= 0
    ) {
      return next.handle(request);
    }

    return next
      .handle(
        request.clone({
          setHeaders: {
            Authorization: `Bearer ${this.authService.getToken()}`
          }
        })
      )
      .pipe(
        tap(
        (event: HttpEvent<any>) => {},
        (err: any) => {
          if (err instanceof HttpErrorResponse) {
            if (err.status === 401) {
              this.authService.logout();
            }
          }
        }
      ));
  }
}
