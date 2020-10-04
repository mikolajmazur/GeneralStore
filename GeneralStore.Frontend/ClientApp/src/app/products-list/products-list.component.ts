import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../Product';
import { ProductDetails } from '../ProductDetails';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {
  products: Product[];
  productWithDetails: ProductDetails;

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.getProducts();
    this.getProduct();
  }

  getProducts(): void {
    this.productService.getInitialProducts()
    .subscribe(products => this.products = products.content);
  }

  getProduct(): void {
    this.productService.getProductDetails("E8AAB2B0-3092-4B63-A128-41730F06CB80")
      .subscribe(product => { this.productWithDetails = product; console.log(this.productWithDetails); });
  }
}
