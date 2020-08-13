import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';
import { Observable } from 'rxjs';
import { ShopUser, UserService } from 'src/app/services/user.service';
import { map } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RefreshService } from 'src/app/services/refresh.service';

@Component({
  selector: 'friday-adjust-user',
  templateUrl: './adjust-user.component.html',
  styleUrls: ['./adjust-user.component.scss']
})
export class AdjustUserComponent implements OnInit {

  users: Observable<ShopUser[]>
  selectedUser: ShopUser
  form: FormGroup

  showError: boolean = false

  hasSubmitted: boolean=false
  check: boolean=false
  success: boolean=false


  constructor(private admin: AdminService, public builder: FormBuilder, private user: UserService, private refresh: RefreshService) {
    //Form building
    this.users = this.admin.getAllUsers()
    this.form = builder.group({
      selection: builder.control('', Validators.required),
      currentBalance: builder.control({ value: 0, disabled: true }),
      balance: builder.control(0, Validators.required),
      newBalance: builder.control({ value: 0, disabled: true })//Not sponsored though
    })

    //Form stuff
    this.form.get('selection').valueChanges.subscribe((s: ShopUser) => {
      this.selectedUser = s
      this.form.get('currentBalance').setValue(s.balance)
      this.form.get('newBalance').setValue(s.balance)
    })
    this.form.get('balance').valueChanges.subscribe(s =>
      this.form.get('newBalance')
        .setValue(reduceToDecimals(+this.form.get('currentBalance').value + +s))
    )

    //Refresh
    this.refresh.refresh.subscribe(s => this.users = this.admin.getAllUsers())

  }

  ngOnInit() {
  }

  submit() {
    if (!this.form.valid) {
      this.showError = true
      return
    }

    this.hasSubmitted=true

    let name = this.selectedUser.name
    let amount = this.form.get('balance').value

    this.user.updateBalance(name, amount).subscribe(s=> {
      
    })

    this.users = this.admin.getAllUsers()

    this.form.updateValueAndValidity()


  }

}

function reduceToDecimals(value: number) {
  return value.toFixed(2)
}

