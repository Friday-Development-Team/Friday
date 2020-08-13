import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreContainerComponent } from './store-container/store-container.component';
import { UserService } from '../services/user.service';
import { ShopcontainerComponent } from './shop/shopcontainer/shopcontainer.component';
import { FilterscontainerComponent } from './shop/filters/filterscontainer/filterscontainer.component';
import { CartcontainerComponent } from './shop/cart/cartcontainer/cartcontainer.component';
import { CartComponent } from './shop/cart/cart/cart.component';
import { FiltersComponent } from './shop/filters/filters/filters.component';
import { ShowcaseComponent } from './shop/showcase/showcase/showcase.component';
import { ItemboxComponent } from './shop/showcase/itembox/itembox.component';
import { SearchbarsComponent } from './shop/showcase/searchbars/searchbars.component';
import { HistoryComponent } from './history/history/history.component';
import { OrdersComponent } from './orders/orders/orders.component';
import { Routes, RouterModule } from '@angular/router';
import { ToolscontainerComponent } from './tools/toolscontainer/toolscontainer.component';
import { AdmintoolsComponent } from './tools/admintools/admintools.component';
import { CateringtoolsComponent } from './tools/cateringtools/cateringtools.component';
import { AuthGuard } from '../authentication/auth.guard';
import { PricefilterPipe } from '../pipes/pricefilter.pipe';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SearchPipe } from '../pipes/search.pipe';
import { OrderPipe } from '../pipes/order.pipe';
import { RoleGuard } from './guards/role.guard';
import { RunningComponent } from './running/running.component';
import { AdjustUserComponent } from './tools/toolcomponents/admin/adjust-user/adjust-user.component';
import { AdminBaseComponent } from './tools/toolcomponents/admin/admin-base/admin-base.component';
import { TwoDigitDecimalNumberDirective } from './directives/two-digit-decimal-nummer.directive';
import { AdduserComponent } from './tools/toolcomponents/admin/adduser/adduser.component';
import { LogsComponent } from './tools/toolcomponents/admin/logs/logs.component';
import { ConfigComponent } from './tools/toolcomponents/admin/config/config.component';
import { AdditemComponent } from './tools/toolcomponents/catering/additem/additem.component';
import { ManagestockComponent } from './tools/toolcomponents/catering/managestock/managestock.component';
import { TotalhistoryComponent } from './tools/toolcomponents/catering/totalhistory/totalhistory.component';
import { CateringComponent } from './catering/catering.component';

const routes: Routes = [
  {
<<<<<<< HEAD
    path: 'store', children: [
      { path: 'store/', redirectTo: 'shop', pathMatch: 'full' },
=======
    path: 'store', component: StoreContainerComponent, canActivate: [AuthGuard], children: [
      { path: '', redirectTo: 'shop', pathMatch: 'full' },
>>>>>>> 09c26935c2c00a2d0e07de14afd650d487d5818d
      { path: 'shop', component: ShopcontainerComponent },
      { path: 'history', component: HistoryComponent },
      { path: 'running', component: RunningComponent },
      { path: 'orders', component: OrdersComponent },
      { path: 'catering', component: CateringComponent, canActivate: [RoleGuard], data: { role: ['admin', 'catering'] } },
      {
        path: 'tools', component: ToolscontainerComponent, canActivate: [RoleGuard], data: { role: ['admin', 'catering'] }, children: [
          {
            path: 'admin', component: AdmintoolsComponent, canActivate: [RoleGuard], data: { role: ['admin'] }, children: [
              // { path: '', redirectTo: 'adduser', pathMatch: 'full' },//Not needed, let user select first
              { path: 'adduser', component: AdduserComponent },
              { path: 'adjustuser', component: AdjustUserComponent },
              { path: 'logs', component: LogsComponent },
              { path: 'config', component: ConfigComponent }
            ]
          },
          {
            path: 'catering', component: CateringtoolsComponent, canActivate: [RoleGuard], data: { role: ['catering'] }, children: [
              { path: 'additem', component: AdditemComponent },
              { path: 'managestock', component: ManagestockComponent },
              { path: 'history', component: TotalhistoryComponent }
            ]
          }
        ]
      }
    ]
  }
];

@NgModule({
  declarations: [
    StoreContainerComponent,
    ShopcontainerComponent,
    FilterscontainerComponent,
    CartcontainerComponent,
    CartComponent,
    FiltersComponent,
    ShowcaseComponent,
    ItemboxComponent,
    SearchbarsComponent,
    HistoryComponent,
    OrdersComponent,
    ToolscontainerComponent,
    AdmintoolsComponent,
    CateringtoolsComponent,
    PricefilterPipe,
    SearchPipe,
    OrderPipe,
    RunningComponent,
    AdjustUserComponent,
    AdminBaseComponent,
    TwoDigitDecimalNumberDirective,
    AdduserComponent,
    LogsComponent,
    ConfigComponent,
    AdditemComponent,
    ManagestockComponent,
    TotalhistoryComponent,
    CateringComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    RouterModule
  ],
  providers: [
    UserService,
    AuthGuard,
    RoleGuard,
    PricefilterPipe,
    SearchPipe,
    OrderPipe
  ],
  exports: [RouterModule]
})
export class StoreModule { }
