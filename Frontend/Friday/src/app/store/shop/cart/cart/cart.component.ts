import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Cart } from 'src/app/models/models';

@Component({
  selector: 'friday-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  @Input() cart: Cart
  @Output() onRemove: EventEmitter<number> = new EventEmitter()
  @Output() onOrderPlace: EventEmitter<any> = new EventEmitter()
  @Output() onCartClear: EventEmitter<any> = new EventEmitter()

  constructor() { }

  ngOnInit() {
  }

  deleteItem(name: number) {
    this.onRemove.emit(name)
  }

  placeOrder() {
    this.onOrderPlace.emit()
  }

  clearCart() {
    this.onCartClear.emit()
  }

}
