import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.apiUrl;
  validationError: string[] = [];

  constructor(private http: HttpClient) { }
  
  ngOnInit(): void {
    
  }

  get404Error() {
    this.http.get(this.baseUrl + 'products/42').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    });
  }

  get500Error() {
    this.http.get(this.baseUrl + 'buggy/servererror').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    });
  }

  get400BadRequestError() {
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    });
  }

  get400ValidationError() {
    this.http.get(this.baseUrl + 'products/fourtytwo').subscribe({
      next: response => console.log(response),
      error: error => {
        console.log(error);
        this.validationError = error.errors
      }
    });
  }

}
