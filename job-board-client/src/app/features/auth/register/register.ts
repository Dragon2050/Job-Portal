import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class RegisterComponent {
  private fb = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  // 1. We define a default role signal (starts as Candidate)
  selectedRole = signal<'Candidate' | 'Recruiter'>('Candidate');

  isLoading = signal(false);
  errorMessage = signal<string | null>(null);
  successMessage = signal<string | null>(null);

  // 2. Define the registration form controls and validations
  registerForm: FormGroup = this.fb.group({
    fullName: ['', [Validators.required, Validators.minLength(3)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  // 3. Toggle role selection helper
  setRole(role: 'Candidate' | 'Recruiter'): void {
    this.selectedRole.set(role);
  }

  // 4. Handle Sign Up submission
  onSubmit(): void {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched(); // Highlight validation errors
      return;
    }
    this.isLoading.set(true);
    this.errorMessage.set(null);
    // Merge form values with the selected role state
    const payload = {
      ...this.registerForm.value,
      role: this.selectedRole()
    };

    // Send Payload to the C# Backend
    this.authService.register(payload).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.successMessage.set('Registration successful! You can now log in.');

        // wait 2 seconds so the user can read the seuccess message before redirecting
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      },
      error: (err) => {
        this.isLoading.set(false);
        // Display backend error message (e.g., email already exists)
        this.errorMessage.set(err.error?.message || 'Registration failed. Please try again.');
      }
    });
  }
}
