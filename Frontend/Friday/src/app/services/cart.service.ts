import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { Cart, Item, OrderItem } from '../models/models';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private cart: Cart
  cartItems: BehaviorSubject<Cart> = new BehaviorSubject(new Cart())

  constructor(private data: DataService, private user: UserService) {
    this.cart = new Cart()
  }

  addToCart(item: Item, amount: number) {
    console.log(`Amount = ${amount}`)
    let id = this.cart.items.map(s => { return s.item.id }).indexOf(item.id)
    if (id === -1)
      this.cart.items.push(new OrderItem(item, amount))
    else {
      var temp: number = +this.cart.items[id].amount + +amount
      this.cart.items[id].amount = temp
    }
    this.cartItems.next(this.cart)

    this.cart.updateTotal()
    console.log(this.cart)
  }

  removeFromCart(item: number) {
    let id = this.cart.items.map(s => s.item.id).indexOf(item)
    if (id === -1)
      return
    if (this.cart.items[id].amount === 1)
      this.cart.items.splice(id)
    else
      this.cart.items[id].amount--;

    this.cart.updateTotal()

    this.cartItems.next(this.cart)
  }

  flushCart() {
    this.cart = new Cart()
    this.pushCart()
  }

  placeOrder(): boolean {
    var response: number
    this.data.placeOrder(new OrderDTO(this.cart.items.map(s => { return new OrderItemDTO(s.item.id, s.amount) }))).subscribe(s => response = s)
    this.flushCart()
    if (!!response)
      this.user.startOrderPolling()
    return !!response
  }

  private pushCart() {
    this.cartItems.next(this.cart)
  }
}

export class OrderDTO {
  constructor(public items: OrderItemDTO[]) { }
}

export class OrderItemDTO {
  constructor(public id: number, public amount: number) { }
}

