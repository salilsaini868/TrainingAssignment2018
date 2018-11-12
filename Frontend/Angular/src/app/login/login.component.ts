import { Component, OnInit } from '@angular/core';
import { SigninService } from '../services/login.service';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { HttpResponse } from '@angular/common/http/src/response';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  submitted = false;
  errorCheck = false;
  error = '';

  constructor(
    private router: Router,
    private SignInService: SigninService) { }

  ngOnInit() { }

  login(form: NgForm) {
    this.submitted = true;
    if (form.valid) {
      this.SignInService.login(this.model.username, this.model.password)
        .subscribe(result => {
          console.log(result);
          if (result.statusCode === 200) {
            console.log(result.value.token);
            localStorage.setItem('token', JSON.stringify((result.value.token)));
            this.router.navigate(['dashboard']);
          } else {
            this.errorCheck = true;
            this.error = 'Username or Password is incorrect';
          }
        }, error => {
           console.log('error: ' + error);
        });
    }
  }
}
