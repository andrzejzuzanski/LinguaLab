import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material.module';
import { AuthService } from './core/services/auth.service';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  imports: [RouterModule, MaterialModule, CommonModule],
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'LinguaLab.Client';
  private authService = inject(AuthService);
  isLoggedIn$: Observable<boolean>;

  constructor() {
    this.isLoggedIn$ = this.authService.loggedIn$;
  }

  logout(): void {
    this.authService.logout();
  }
}
