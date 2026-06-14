import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})

export class HeaderComponent {
  // Inject the auth service to access user signals directly
  protected readonly authService = inject(AuthService);
  // Expose signals to the HTML template
  readonly currentUser = this.authService.currentUser;
  readonly isLoggedIn = this.authService.isLoggedIn;
  readonly userRole = this.authService.userRole;
  // Handle logout
  onLogout(): void {
    this.authService.logout();
  }
}
