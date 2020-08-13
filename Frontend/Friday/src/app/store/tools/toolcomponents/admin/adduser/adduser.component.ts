import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Observable } from 'rxjs';
import { ValidatorFn, AbstractControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { map } from 'rxjs/operators';

@Component({
  selector: 'friday-adduser',
  templateUrl: './adduser.component.html',
  styleUrls: ['./adduser.component.scss']
})
export class AdduserComponent implements OnInit {

  hasSubmitted = false
  form: FormGroup

  check: boolean=false
  success: boolean=false

  constructor(private auth: AuthService, builder: FormBuilder) {
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

  register() {
    this.hasSubmitted = true
    if (this.form.invalid) {
      return
    }

    let name = this.form.get('username').value
    let pass = this.form.get('passwordgroup').get('password').value

    let temp=this.auth.register(name, pass)
    console.log(temp)
    temp.subscribe(s=> this.success=s)
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
          return null
        }
        return { userAlreadyExists: true }
      })
    );
  };
}
