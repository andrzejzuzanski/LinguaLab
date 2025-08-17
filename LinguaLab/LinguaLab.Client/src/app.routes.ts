import { Routes } from '@angular/router';

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
  },

  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
];
