import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../app/environments';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllEmployees(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Employees`);
  }

  getAllShippers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Shippers`);
  }

  getAllProducts(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Products`);
  }
}