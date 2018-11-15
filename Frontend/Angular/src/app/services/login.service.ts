import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { debuglog } from 'util';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SigninService {

  constructor(private http: HttpClient) {
  }
  login(username: string, password: string): Observable<any> {
    const queryParameters = new HttpParams().set('username', username).set('password', password);
    console.log(queryParameters.toString());
    return this.http.get('http://localhost:50100/api/login', { params: queryParameters });
  }
  logout() {
    localStorage.removeItem('token');
  }
}
