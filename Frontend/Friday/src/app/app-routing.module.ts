import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotfoundComponent } from './notfound/notfound.component';

const routes: Routes = [
<<<<<<< HEAD
  //{ path: '**', component: NotfoundComponent },
  { path: '', redirectTo: 'auth', pathMatch: 'full' }
=======
  { path: '', redirectTo: 'store', pathMatch: 'full' },
  //{ path: '**', component: NotfoundComponent }
>>>>>>> 09c26935c2c00a2d0e07de14afd650d487d5818d
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
