import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private apiUrl = 'http://localhost:5282/api/categories';
  private http = inject(HttpClient);

  getCategories(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
