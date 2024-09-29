import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../app/environments';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = `${environment.apiUrl}/Orders`;

  constructor(private http: HttpClient) { }

  getOrdersByCustomerId(custId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/by-customer/${custId}`);
  }

  createOrder(orderData: any): Observable<number> {
    return this.http.post<number>(this.apiUrl, orderData);
  }
}