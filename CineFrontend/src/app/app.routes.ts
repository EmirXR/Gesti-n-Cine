import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login';
import { DashboardComponent } from './components/dashboard/dashboard';
import { PeliculasComponent } from './components/peliculas/peliculas';
import { AsignacionComponent } from './components/asignacion/asignacion';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
  { path: 'peliculas', component: PeliculasComponent, canActivate: [authGuard] },
  { path: 'asignacion', component: AsignacionComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: 'login' }
];