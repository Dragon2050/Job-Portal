import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';


// 1. General Guard: Verifies if a user is logged in
export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  if (authService.isLoggedIn()) {
    return true; // User is logged in, allow access
  }
  // User is not logged in, redirect to login page
  router.navigate(['/login']);
  return false; // Block access
}

// 2. Role Guard: Verifies if the logged-in user is a Recruiter
export const recruiterGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  if (authService.isLoggedIn() && authService.userRole() === 'Recruiter') {
    return true; // User is logged in and is a recruiter, allow access
  }

  // User is not logged in or not a recruiter, redirect to login page
  router.navigate(['/login']);
  return false; // Block access
}
