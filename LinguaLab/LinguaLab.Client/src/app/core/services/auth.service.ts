import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5282/api/auth';
  private router = inject(Router);
  private http = inject(HttpClient);

  constructor() {}

  private loggedIn = new BehaviorSubject<boolean>(this.isLoggedIn());
  loggedIn$ = this.loggedIn.asObservable();

  login(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, data).pipe(
      tap((response) => {
        if (response && response.token) {
          this.setToken(response.token);
          this.loggedIn.next(true);
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

  public isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  logout(): void {
    localStorage.removeItem('auth_token');
    this.loggedIn.next(false);
    this.router.navigate(['/auth/login']);
  }
}
