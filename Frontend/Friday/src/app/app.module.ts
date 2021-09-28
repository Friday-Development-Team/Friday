import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthenticationModule } from './authentication/authentication.module';
import { NotfoundComponent } from './notfound/notfound.component';
import { StoreModule } from './store/store.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { httpInterceptorProviders } from './interceptors';
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
    NgbModule
  ],
  providers: [
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    httpInterceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
