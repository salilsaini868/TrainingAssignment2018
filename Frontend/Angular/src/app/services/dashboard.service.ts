import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class DashboardService {

    constructor(private http: HttpClient) { }

    Dashboard() {
        return this.http.get('http://localhost:50100/api/dashboard');
    }
}



