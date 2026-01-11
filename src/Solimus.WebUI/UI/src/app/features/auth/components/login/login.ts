import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TuiButton, TuiError, TuiIcon, TuiLabel, TuiTextfield } from '@taiga-ui/core';
import { TuiFieldErrorPipe } from '@taiga-ui/kit';
import { AuthService } from '../../../../core/auth/services/auth.service';
import { LoginRequest, LoginResponse } from '../../../../core/auth/models/auth.models';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    TuiButton,
    TuiIcon,
    TuiError,
    TuiLabel,
    TuiTextfield,
    TuiFieldErrorPipe
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  private readonly fb = inject(FormBuilder);
  protected authService = inject(AuthService);

  isAuthenticated = this.authService.isAuthenticated;
  currentUser = this.authService.currentUser;

  showPassword = false;

  readonly loginForm: FormGroup = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const credentials: LoginRequest = {
        email: this.loginForm.controls['email'].value,
        password: this.loginForm.controls['password'].value
      };
      this.authService.login(credentials).subscribe({
        next: (response: LoginResponse) => {
          console.log(response); //Нужно тут редиректить
        },
        error: (errorResponse: HttpErrorResponse) => {
          console.log(errorResponse); //Нужно как то пользователю показать ошибку.
        }
      });
    } else {
      this.loginForm.markAllAsTouched();
    }
  }
}
