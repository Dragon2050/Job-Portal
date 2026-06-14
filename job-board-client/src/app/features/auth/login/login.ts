import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, RouterLink],
    templateUrl: './login.html',
    styleUrl: './login.scss'
})
export class LoginComponent {
    private fb = inject(FormBuilder);
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);
    // 1. Define the reactive form structure with validators
    loginForm: FormGroup = this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]]
    });

    // 2. Local signals for loading state and error feedback
    isLoading = signal(false);
    errorMessage = signal<string | null>(null);
    // 3. Handle Form Submission
    onSubmit(): void {
        if (this.loginForm.invalid) {
            this.loginForm.markAllAsTouched(); // Highlights validation errors in the UI
            return;
        }
        this.isLoading.set(true);
        this.errorMessage.set(null);
        // Call the login method from our AuthService
        this.authService.login(this.loginForm.value).subscribe({
            next: () => {
                this.isLoading.set(false);
                this.router.navigate(['/']); // Redirect to home page on success
            },
            error: (err) => {
                this.isLoading.set(false);
                // Extract error message sent by C# backend
                this.errorMessage.set(err.error?.message || 'Invalid email or password.');
            }
        });
    }
}
