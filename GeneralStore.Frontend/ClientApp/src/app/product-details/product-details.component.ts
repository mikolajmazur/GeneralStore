import { Component, OnInit } from '@angular/core';
import { ProductDetails } from '../ProductDetails';
import { ProductService } from '../product.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  product$: Observable<ProductDetails>;

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.getProduct();
  }

  getProduct(): void {
    this.product$ = this.productService.getProductDetails("E8AAB2B0-3092-4B63-A128-41730F06CB80");;
  }
}
