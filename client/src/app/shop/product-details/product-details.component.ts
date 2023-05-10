import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/Models/products';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product?: IProduct;

  constructor(
    private oneProduct: ShopService,
    private active: ActivatedRoute,
    private BcService: BreadcrumbService
  ) {
    this.BcService.set('@productDetails', ' ');
  }
  ngOnInit(): void {
    this.loadProduct();
  }
  loadProduct() {
    const id = this.active.snapshot.paramMap.get('id');
    if (id)
      this.oneProduct.getProduct(+id).subscribe((res) => {
        this.product = res;
        this.BcService.set('@productDetails', this.product.name);
        console.log(res);
      });
  }
}
