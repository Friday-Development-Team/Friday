import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthenticationModule } from './authentication/authentication.module';
import { NotfoundComponent } from './notfound/notfound.component';
import { StoreModule } from './store/store.module';
<<<<<<< HEAD
import { FormsModule } from '@angular/forms';
=======
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { httpInterceptorProviders } from './interceptors';
import { registerLocaleData } from '@angular/common';
>>>>>>> 09c26935c2c00a2d0e07de14afd650d487d5818d
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    NotfoundComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthenticationModule,
    StoreModule,
<<<<<<< HEAD
    NgbModule
  ],
  providers: [FormsModule],
=======
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [httpInterceptorProviders],
>>>>>>> 09c26935c2c00a2d0e07de14afd650d487d5818d
  bootstrap: [AppComponent]
})
export class AppModule { }
