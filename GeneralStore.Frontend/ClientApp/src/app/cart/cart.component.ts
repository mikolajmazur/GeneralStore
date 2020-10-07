import { Component, OnInit } from '@angular/core';
import { CartService } from '../cart.service';
import { CartItem } from '../CartItem';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  products: CartItem[];
  total: number = 0;

  constructor(private cartService: CartService) { }

  ngOnInit() {
    this.getInitialCart();
    this.calculateTotal();
  }
    calculateTotal() {
      for (var item of this.products) {
        this.total += item.price * item.amount;
      }
    }

  getInitialCart(): void {
    this.products = this.cartService.getItems();
  }

  removeItem(item: CartItem) {
  }

  clearCart(): void {
    this.cartService.clearCart();
    this.products = [];
  }
}
