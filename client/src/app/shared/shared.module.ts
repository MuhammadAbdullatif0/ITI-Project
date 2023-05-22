import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import { RouterModule } from '@angular/router';
import { OrderTotalsComponent } from './order-totals/order-totals.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './components/text-input/text-input.component';

@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    BasketSummaryComponent,
    OrderTotalsComponent,
    TextInputComponent,
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    RouterModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    BasketSummaryComponent,
    OrderTotalsComponent,
    ReactiveFormsModule,
    TextInputComponent,
    BsDropdownModule,
  ],
})
export class SharedModule {}
