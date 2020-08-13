import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthService } from '../services/auth.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
<<<<<<< HEAD
=======
import { HttpClientModule } from '@angular/common/http';

>>>>>>> 09c26935c2c00a2d0e07de14afd650d487d5818d

const routes: Routes = [
  {
    path: 'auth', children: [
<<<<<<< HEAD
      { path: 'auth/login', component: LoginComponent },
      { path: 'auth/register', component: RegisterComponent },
      { path: '', redirectTo: 'auth/login', pathMatch: 'full' }
=======
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: '', redirectTo: 'login', pathMatch: 'full' }
>>>>>>> 09c26935c2c00a2d0e07de14afd650d487d5818d
    ]
  }
];

@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
<<<<<<< HEAD
    FormsModule, ReactiveFormsModule
  ],
  exports: [
    RouterModule
=======
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
>>>>>>> 09c26935c2c00a2d0e07de14afd650d487d5818d
  ],
  providers: [
    AuthService
  ],
  exports: [RouterModule]
})
export class AuthenticationModule { }
