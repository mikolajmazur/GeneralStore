import { Injectable } from '@angular/core';
import { Product } from './product';
import { ProductDetails } from './ProductDetails';
import { ListResult } from "./ListResult";
import { Observable, of } from "rxjs";
import { HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private callUrl: string = "https://localhost:44349/api/v1/categories/ef001e3f-472d-46b1-a8f8-32a15ebbc78b/products";
  private productDetailsUrl: string = "https://localhost:44349/api/v1/products/";

  constructor(private http: HttpClient) { }

  getInitialProducts(): Observable<ListResult<Product[]>> {
    let products: Product[] = [{ id: "guid1", price: 20, name: "product 1" },
    { id: "guid2", price: 50, name: "product 5" }];

    //return of(products);
    return this.http.get<ListResult<Product[]>>(this.callUrl);
  }

  getProductDetails(productGuid: string): Observable<ProductDetails> {
    let url: string = this.productDetailsUrl + productGuid;
    return this.http.get<ProductDetails>(url);
  }
}
