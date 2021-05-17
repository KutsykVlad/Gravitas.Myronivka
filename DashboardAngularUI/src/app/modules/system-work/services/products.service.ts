import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from 'src/app/models/product';
import { environment } from './../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  constructor(private http: HttpClient) {}

  public getProducts(searchedText?: string): Observable<Product[]> {
    let params = new HttpParams();
    if (searchedText) {
      params = params.set('text', searchedText);
    }

    return this.http.get<Product[]>(
      `${environment.apiUrl}dashboard/GetProcessedProducts`,
      { params }
    );
  }

  public getProductData(productsIds: string[], dateFrom?: Date, dateTo?: Date) {
    return this.http.get(`${environment.apiUrl}dashboard/GetProductData`, {
      params: {
        'productIds[]': productsIds,
        from: dateFrom ? dateFrom.toISOString() : null,
        to: dateTo ? dateTo.toISOString() : null
      }
    });
  }
}
