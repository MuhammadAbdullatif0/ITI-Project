import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  count = 0;
  constructor(private spinner: NgxSpinnerService) {}

  busy() {
    this.count++;
    this.spinner.show(undefined, {
      type: 'square-jelly-box',
      bdColor: 'rgba(255,255,255,0.7)',
      color: '#964B00',
    });
  }

  idle() {
    this.count--;
    if (this.count <= 0) {
      this.count = 0;
      this.spinner.hide();
    }
  }
}
