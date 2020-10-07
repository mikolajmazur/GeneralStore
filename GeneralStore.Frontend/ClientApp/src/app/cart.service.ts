import { Injectable } from '@angular/core';
import { CartItem } from './CartItem';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor() { }

  addItem(item: CartItem): void {
    let currentItemsJson = localStorage.getItem("cart");
    let currentItems: [CartItem];
    if (currentItemsJson === null) {
      currentItems = [item];
    } else {
      currentItems = JSON.parse(currentItemsJson);
      let existingItem: CartItem;
      if (existingItem = currentItems.find(i => i.id === item.id)) {
        // item already exists in cart, increment amount
        existingItem.amount++;
      } else {
        currentItems.push(item);
      }
    }
    localStorage.setItem("cart", JSON.stringify(currentItems));
  }

  getItems(): CartItem[] {
    return JSON.parse(localStorage.getItem("cart"));
  }



  clearCart(): void {
    localStorage.removeItem("cart");
  }
}
