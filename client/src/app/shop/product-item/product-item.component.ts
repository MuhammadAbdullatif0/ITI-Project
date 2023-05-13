import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/Models/products';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss'],
})
export class ProductItemComponent {
  @Input() product?: IProduct;

  constructor(private basket: BasketService) {}

  addItemToBasket() {
    this.product && this.basket.addItemToBasket(this.product);
  }
}
