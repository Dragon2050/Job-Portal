import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { User, UserRole } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);

  // Set the backend URL based on the API port from launchSettings.json
  private readonly apiUrl = 'https://localhost:44372/api/auth';

  // 1. Session state using Angular Signals
  readonly currentUser = signal<User | null>(null);

  // 2. Computed signals to track active session indicators
  readonly isLoggedIn = computed(() => !!this.currentUser());
  readonly userRole = computed(() => this.currentUser()?.role || null);

  constructor() {
    this.autoLogin();
  }

  // 3. Register a new user
  register(userData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userData);
  }

  // 4. Log in and initialize session (with browser safety checks)
  login(credentials: any): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, credentials).pipe(
      tap((response: { token: string }) => {
        if (response.token) {
          if (typeof window !== 'undefined') {
            localStorage.setItem('token', response.token);
          }
          const user = this.decodeToken(response.token);
          this.currentUser.set(user);
        }
      })
    );
  }

  // 5. Log out and clear state (with browser safety checks)
  logout(): void {
    if (typeof window !== 'undefined') {
      localStorage.removeItem('token');
    }
    this.currentUser.set(null);
    this.router.navigate(['/login']);
  }

  // 6. Automatically log in on page reload (with browser safety checks)
  private autoLogin(): void {
    if (typeof window === 'undefined') {
      return; // Do not check localStorage on the Node.js server
    }

    const token = localStorage.getItem('token');
    if (token) {
      const user = this.decodeToken(token);
      if (user) {
        this.currentUser.set(user);
      } else {
        this.logout();
      }
    }
  }


  // 7. Decode JWT token payload to read Claims (ID, Email, Role)
  private decodeToken(token: string): User | null {
    try {
      const payloadBase64 = token.split('.')[1];
      const payloadJson = atob(payloadBase64);
      const payload = JSON.parse(payloadJson);

      // Extract claims based on standard .NET ClaimTypes schemas
      const id = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || payload.nameid;
      const email = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] || payload.email;
      const role = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload.role;

      if (!id || !email || !role) {
        return null;
      }

      // Since the backend token contains email but not name, we use the email handle as a temporary display name
      const fallbackName = email.split('@')[0];

      return {
        id,
        email,
        role: role as UserRole,
        fullName: fallbackName.charAt(0).toUpperCase() + fallbackName.slice(1)
      };
    } catch (e) {
      console.error('Failed to decode token:', e);
      return null;
    }
  }
}


