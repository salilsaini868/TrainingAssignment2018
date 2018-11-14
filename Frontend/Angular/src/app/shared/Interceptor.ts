import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';

import { Observable } from 'rxjs';
@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor() { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const currentUser = JSON.parse(localStorage.getItem('token'));
        if (currentUser) {
            request = request.clone({
                setHeaders: {
                    accept: 'application/json',
                   Authorization: `Bearer ${currentUser}`
                }
            });
        }

        return next.handle(request);
    }
}
