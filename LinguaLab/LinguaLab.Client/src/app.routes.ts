import { Routes } from '@angular/router';
import { authGuard } from './app/core/services/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('./app/features/auth/auth.routes').then((r) => r.AUTH_ROUTES),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./app/features/dashboard/dashboard.routes').then(
        (r) => r.DASHBOARD_ROUTES
      ),
    canActivate: [authGuard],
  },
  {
    path: 'vocabulary',
    loadComponent: () =>
      import(
        './app/features/vocabulary/pages/vocabulary-list/vocabulary-list.component'
      ).then((c) => c.VocabularyListComponent),
    canActivate: [authGuard],
  },

  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
];
