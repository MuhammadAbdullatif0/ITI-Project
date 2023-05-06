import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './shared/Models/products';
import { IPagination } from './shared/Models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  products: IProduct[] = [];

  constructor(private http: HttpClient) { }
  
  ngOnInit(): void {

    this.http.get<IPagination<IProduct>>('https://localhost:5001/api/Products?PageSize=50').subscribe({
      
      next: (res: any) => (this.products = res.data),

      error: (error) => console.log(error),
      
      complete: () => {
        console.log('Completed');
        console.log(this.products);
      },
    });
  }
  title = 'client';
}
