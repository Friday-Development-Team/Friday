import { Component, OnInit } from '@angular/core';
import { Cart } from 'src/app/models/models';
import { CartService } from 'src/app/services/cart.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'friday-cartcontainer',
  templateUrl: './cartcontainer.component.html',
  styleUrls: ['./cartcontainer.component.scss']
})
export class CartcontainerComponent implements OnInit {

  cart: Observable<Cart>

  constructor(private cartServ: CartService) {
    this.cart=this.cartServ.cartItems
  }

  ngOnInit() {
  }

  deleteItem(id: number) {
    this.cartServ.removeFromCart(id)
  }

  placeOrder() {
    this.cartServ.placeOrder()
  }

  clearCart() {
    this.cartServ.flushCart()
  }
}
