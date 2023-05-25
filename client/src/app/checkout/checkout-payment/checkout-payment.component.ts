import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { Basket } from 'src/app/shared/Models/basket';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm?: FormGroup;
  constructor(
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  async submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    if (!basket) return;
    const ordertToCreate = this.getOrdToCreate(basket);
    if (!ordertToCreate) return;
    // this.checkoutService.createOrder(ordertToCreate).subscribe({
    //   next: order => {
        
    //   }
    // })
  }
  getOrdToCreate(basket: Basket) {
    const deliveryMethodId = this.checkoutForm
      ?.get('deliveryForm')
      ?.get('deliveryMethod')?.value;
    const shipToAdress = this.checkoutForm?.get('adressForm')?.value;
    if (!deliveryMethodId || !shipToAdress) return;
    return {
      basketId: basket.id,
      deliveryMethodId: deliveryMethodId,
      shipToAdress: shipToAdress,
    };
  }
}
