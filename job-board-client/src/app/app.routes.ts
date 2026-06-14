import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login';
import { RegisterComponent } from './features/auth/register/register';

export const routes: Routes = [
  // 1. Path to render the login page
  { path: 'login', component: LoginComponent },

  // 2. Path to render the register page
  { path: 'register', component: RegisterComponent },

  // 3. Root redirect path
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];
