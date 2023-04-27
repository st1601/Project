import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppService } from './services/app.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private appSerive:AppService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    if (this.appSerive.isLogIn()) {
      let authRequest = request.clone({
        setHeaders: {
          authorization: 'Bearer ' + this.appSerive.getToken(),
        },
      });

      return next.handle(authRequest);
    }
    return next.handle(request);
  }
}
