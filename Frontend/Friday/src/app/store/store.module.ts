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

const routes: Routes = [
  {
    path: 'store', children: [
      { path: 'store/', redirectTo: 'shop', pathMatch: 'full' },
      { path: 'shop', component: ShopcontainerComponent },
      { path: 'history', component: HistoryComponent },
      { path: 'orders', component: OrdersComponent },
      {
        path: 'tools', children: [
          { path: 'admin', component: AdmintoolsComponent },
          { path: 'catering', component: CateringtoolsComponent }
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
    CateringtoolsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ],
  providers: [
    UserService
  ]
})
export class StoreModule { }
