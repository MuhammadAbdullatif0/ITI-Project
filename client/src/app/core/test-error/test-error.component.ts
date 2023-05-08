import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss'],
})
export class TestErrorComponent {
  baseUrl = environment.apiUrl;
  validationErrors: string[] = [];

  constructor(private http: HttpClient) {}

  get404Error() {
    this.http.get(this.baseUrl + 'products/70').subscribe((res) => {
      console.log(res);
    });
  }
  get500Error() {
    this.http.get(this.baseUrl + 'buggy/servererror').subscribe((res) => {
      console.log(res);
    });
  }
  get400Error() {
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe((res) => {
      console.log(res);
    });
  }
  get400ValidationError() {
    this.http.get(this.baseUrl + 'products/fortytwo').subscribe(
      (res) => {
        console.log(res);
      },
      (error) => {
        this.validationErrors = error.error.message;
        console.log(error.error.message);
      }
    );
  }
}
