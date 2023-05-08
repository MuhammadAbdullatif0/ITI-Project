import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopService } from '../shop.service';
import { IProduct } from 'src/app/shared/Models/products';
import { brand } from 'src/app/shared/Models/brand';
import { type } from 'src/app/shared/Models/type';
import { ShopParams } from 'src/app/shared/Models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTearm?: ElementRef;
  products: IProduct[] = [];
  brands: brand[] = [];
  types: type[] = [];
  shopParams = new ShopParams();
  disabled = false;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to high', value: 'priceAsc' },
    { name: 'Price: High to low', value: 'priceDesc' },
  ];
  totalCount = 0;
  shopService: any;
  constructor(private shopProducts: ShopService) {}

  ngOnInit(): void {
    this.getBrands();
    this.getProducts();
    this.getTypes();
  }
  toggleState(): void {
    this.disabled = !this.disabled;
  }
  getProducts() {
    this.shopProducts.getProducts(this.shopParams).subscribe((res) => {
      this.products = res.data;
      this.shopParams.pageNumber = res.pageIndex;
      this.shopParams.pageSize = res.pageSize;
      this.totalCount = res.count;
      // console.log(res);
    });
  }

  getBrands() {
    this.shopProducts.getBrands().subscribe((res) => {
      this.brands = [{ id: 0, name: 'All' }, ...res];
      
    });
  }
  getTypes() {
    this.shopProducts
      .getTypes()
      .subscribe((res) => (this.types = [{ id: 0, name: 'All' }, ...res]));
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onSortSelected(event: any) {
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }
  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }
  onSearch() {
    this.shopParams.search = this.searchTearm?.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onReset() {
    if (this.searchTearm) this.searchTearm.nativeElement.value = '';
    this.shopParams.pageNumber = 1;
    this.shopParams = new ShopParams();
    this.shopService.setShopParams(this.shopParams);
    this.getProducts();
  }
}
