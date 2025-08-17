import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  private apiUrl = 'http://localhost:5282/api/analytics';
  private http = inject(HttpClient);

  getActivityHeatmap(): Observable<any> {
    return this.http.get(`${this.apiUrl}/heatmap`);
  }
}
