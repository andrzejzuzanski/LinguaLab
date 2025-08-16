import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5282/api/auth';

  constructor(private http: HttpClient) {}

  login(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, data).pipe(
      // 'tap' pozwala wykonać akcję uboczną (np. zapis do localStorage)
      // bez modyfikowania samej odpowiedzi, która płynie dalej do komponentu.
      tap((response) => {
        if (response && response.token) {
          this.setToken(response.token);
        }
      })
    );
  }

  private setToken(token: string): void {
    localStorage.setItem('auth_token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('auth_token');
  }

  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  logout(): void {
    localStorage.removeItem('auth_token');
  }
}
