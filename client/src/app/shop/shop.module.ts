import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop/shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { ShopRoutingModule } from './shop-routing.module';

@NgModule({
  declarations: [ShopComponent, ProductItemComponent, ProductDetailsComponent],
  imports: [CommonModule, SharedModule, RouterModule, ShopRoutingModule],
})
export class ShopModule {}
