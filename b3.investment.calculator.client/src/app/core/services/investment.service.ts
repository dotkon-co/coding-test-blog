import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { InvestmentRequest, InvestmentResponse } from '../../shared/models/investment.model';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InvestmentService {
  private readonly API_URL = 'http://localhost:5291/cdbcalculation';

  constructor(private http: HttpClient) {}

  calculateInvestment(
    request: InvestmentRequest
  ): Observable<InvestmentResponse> {
    return this.http.post<InvestmentResponse>(this.API_URL, request);
  }
}
