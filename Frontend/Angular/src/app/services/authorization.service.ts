import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

@Injectable()
export class AuthService implements CanActivate {

    constructor(public router: Router) { }

    public canActivate(): boolean {
        debugger;
        const token = localStorage.getItem('token');
        if (!token) {
            this.router.navigate(['']);
            return false;
        }
        return true;
    }
}
