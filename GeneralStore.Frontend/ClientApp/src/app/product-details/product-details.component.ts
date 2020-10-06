import { Component, OnInit } from '@angular/core';
import { ProductDetails } from '../ProductDetails';
import { ProductService } from '../product.service';
import { Observable } from 'rxjs';
import { CartService } from '../cart.service';
import { CartItem } from '../CartItem';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  product$: Observable<ProductDetails>;

  constructor(private productService: ProductService,
    private cartService: CartService) { }

  ngOnInit() {
    this.getProduct();
  }

  getProduct(): void {
    this.product$ = this.productService.getProductDetails("E8AAB2B0-3092-4B63-A128-41730F06CB80");;
  }

  testWrite(): void {
    console.log("test write");
    let item: CartItem = { id: "asf", price: 20, name: "smth shitty", amount: 2 };
    this.cartService.addItem(item);
  }

  testRead(): void {
    let items: CartItem[] = this.cartService.getItems();
    for (var item of items) {
      console.log(item);
    }
  }
}
