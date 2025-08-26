import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LearningService {
  private apiUrl = 'http://localhost:5282/api/learning';
  private http = inject(HttpClient);

  getSessionWords(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + '/session');
  }

  submitAnswer(wordId: string, quality: number): Observable<any> {
    const payload = { wordId, quality };
    return this.http.post(`${this.apiUrl}/answer`, payload);
  }
}
