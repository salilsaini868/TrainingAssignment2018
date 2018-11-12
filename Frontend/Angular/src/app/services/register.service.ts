import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { LoginUser } from '../shared/login-model';
@Injectable({
    providedIn: 'root'
})
export class RegisterService {

    selectedUser: LoginUser = {
        userID: null,
        firstName: '',
        lastName: '',
        username: '',
        password: ''
    };
    constructor(private http: Http) { }

    postLogin(user: LoginUser) {
        const body = JSON.stringify(user);
        const headerOptions = new Headers({ 'Content-Type': 'application/json' });
        const requestOptions = new RequestOptions({ method: RequestMethod.Post, headers: headerOptions });
        return this.http.post('http://localhost:50100/api/user', body, requestOptions).pipe(map(x => {
            return x.json();
        }));
    }
}



