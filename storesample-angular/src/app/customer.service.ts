import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../app/environments';

export interface CustomerPrediction {
  custId: number;
  companyName: string;
  lastOrderDate: string;
  nextPredictedOrder: string;
}

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = `${environment.apiUrl}/Customers/with-predictions`;

  constructor(private http: HttpClient) { }

  getCustomers(page: number, pageSize: number, sortColumn: string, sortDirection: string, searchTerm: string): Observable<{ items: CustomerPrediction[], totalCount: number }> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString())
      .set('sortColumn', sortColumn)
      .set('sortDirection', sortDirection)
      .set('searchTerm', searchTerm);

      
    return this.http.get<CustomerPrediction[]>(this.apiUrl, { params, observe: 'response' })
      .pipe(
        map(response => ({
          items: response.body || [],
          totalCount: parseInt(response.headers.get('X-Total-Count') || '0', 10)
        }))
      );
  }

  
}