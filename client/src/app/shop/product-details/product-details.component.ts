import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/Models/products';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product?: IProduct;

  constructor(
    private oneProduct: ShopService,
    private active: ActivatedRoute
  ) {

  }
  ngOnInit(): void {
    this.loadProduct();
  }
  loadProduct() {
    const id = this.active.snapshot.paramMap.get('id');
    if (id)
      this.oneProduct.getProduct(+id).subscribe((res) => {
        this.product = res;
        console.log(res);
      });
  }
}
