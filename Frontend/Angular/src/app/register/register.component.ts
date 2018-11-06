import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterService } from '../services/register.service';
import { delay } from 'q';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form: FormGroup;
  submitted = false;
  loading = false;
  isReset = false;
  constructor(private registerService: RegisterService, private formBuilder: FormBuilder) { }

  ngOnInit() { }

  submitForm(form: NgForm) {
    if (form.valid) {
      this.registerService.postLogin(form.value)
        .subscribe(data => {
          this.resetForm(form);
        });
    }
    this.submitted = true;
  }

  resetForm(form: NgForm) {
    if (form != null) {
      form.reset();
      this.submitted = false;
    }
    this.registerService.selectedUser = {
      userID: null,
      firstName: '',
      lastName: '',
      username: '',
      password: ''
    };
  }
}
