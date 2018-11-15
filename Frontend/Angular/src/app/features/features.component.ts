import { Component, OnInit } from '@angular/core';
import { SigninService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-features',
  templateUrl: './features.component.html',
  styleUrls: ['./features.component.css']
})
export class FeaturesComponent implements OnInit {

  constructor(private logoutService: SigninService,
    private router: Router) { }

  ngOnInit() {
  }
  logout() {
    this.logoutService.logout();
    this.router.navigate(['']);
  }
}
