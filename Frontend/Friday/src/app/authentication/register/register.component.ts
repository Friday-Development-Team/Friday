import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidatorFn } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'friday-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  hasSubmitted = false
  form: FormGroup

  constructor(private auth: AuthService, builder: FormBuilder, private router: Router) {
    this.form = builder.group({
      username: builder.control('', [Validators.required, serverSideValidateUsername(this.auth.checkUserNameAvailability)]),
      passwordgroup: builder.group({
        password: builder.control('', [Validators.required, Validators.minLength(6)]),
        confirmpassword: builder.control('', Validators.required)
      }, { validator: comparePasswords }
      )
    })
  }

  ngOnInit() {
  }

  onSubmit() {
    if (!this.form.invalid)
      //auth.login()
      console.log("login")
    this.hasSubmitted = true
  }

  toLogin() {
    this.router.navigate(['/auth/login'])
  }

}

function comparePasswords(control: AbstractControl): { [key: string]: any } {
  const password = control.get('password');
  const confirmpassword = control.get('confirmpassword');
  return password.value === confirmpassword.value
    ? null
    : { passwordsDiffer: true };
}

function serverSideValidateUsername(
  checkAvailabilityFn: (n: string) => Observable<boolean>
): ValidatorFn {
  return (control: AbstractControl): Observable<{ [key: string]: any }> => {
    return checkAvailabilityFn(control.value).pipe(
      map(available => {
        if (available) {
          return null;
        }
        return { userAlreadyExists: true };
      })
    );
  };
}