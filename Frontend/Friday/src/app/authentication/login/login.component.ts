import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
<<<<<<< HEAD
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
=======
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';
>>>>>>> 09c26935c2c00a2d0e07de14afd650d487d5818d

@Component({
  selector: 'friday-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

<<<<<<< HEAD
  private form: FormGroup

  constructor(private auth: AuthService, builder: FormBuilder) {
=======
  hasSubmitted = false
  form: FormGroup

  constructor(private auth: AuthService, builder: FormBuilder, private router: Router, private data: DataService) {
>>>>>>> 09c26935c2c00a2d0e07de14afd650d487d5818d
    this.form = builder.group({
      username: builder.control('', Validators.required),
      password: builder.control('', [Validators.required, Validators.minLength(6)])
    })
<<<<<<< HEAD
=======


>>>>>>> 09c26935c2c00a2d0e07de14afd650d487d5818d
  }

  ngOnInit() {
  }

  onSubmit() {
    var username = this.form.value.username
    var password = this.form.value.password
    this.hasSubmitted = true;
    if (this.form.valid)
      this.auth
        .login(this.form.value.username, this.form.value.password)
        .subscribe(
          val => {
            if (val) {
              if (this.auth.redirectUrl) {
                this.router.navigateByUrl(this.auth.redirectUrl)
                this.auth.redirectUrl = undefined
              } else {
                this.router.navigate(['/store'])
              }
            }
          }
        );
  }
}
