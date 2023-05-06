import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/Models/pagination';
import { IProduct } from '../shared/Models/products';
import { brand } from '../shared/Models/brand';
import { type } from '../shared/Models/type';
import { ShopParams } from '../shared/Models/shopParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();
    if (shopParams.brandId > 0)
      params = params.append('brandId', shopParams.brandId);
    if (shopParams.typeId) params = params.append('typeId', shopParams.typeId);
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber);
    params = params.append('pageSize', shopParams.pageSize);
    if (shopParams.search) params = params.append('Search', shopParams.search);
    return this.http.get<IPagination<IProduct[]>>(this.baseUrl + 'products', {
      params,
    });
  }

  getBrands() {
    return this.http.get<brand[]>(this.baseUrl + 'products/brands');
  }
  getTypes() {
    return this.http.get<type[]>(this.baseUrl + 'products/types');
  }
}