import { AsyncPipe, CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TuiButton, TuiError, TuiIcon, TuiLabel, TuiTextfield } from '@taiga-ui/core';
import { TUI_VALIDATION_ERRORS, TuiCheckbox, TuiFieldErrorPipe } from '@taiga-ui/kit';
import { AuthService, RegistrationRequest } from '@core/index';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    TuiButton,
    TuiIcon,
    TuiError,
    TuiLabel,
    TuiTextfield,
    TuiCheckbox,
    TuiFieldErrorPipe,
    AsyncPipe
  ],
  templateUrl: './register.html',
  styleUrl: './register.css',
  providers: [
    {
      provide: TUI_VALIDATION_ERRORS,
      useValue: {
        required: 'Обязательное поле',
        email: 'Некорректный email',
        pattern: 'Некорректный формат'
      }
    }
  ]
})
export class Register {
  private readonly fb = inject(FormBuilder);
  private readonly authService = inject(AuthService);

  showPassword = false;
  showConfirmPassword = false;

  readonly registerForm: FormGroup = this.fb.group({
    firstName: ['', [Validators.required, Validators.minLength(2)]],
    lastName: ['', [Validators.required, Validators.minLength(2)]],
    userName: ['', [Validators.required, Validators.minLength(3), Validators.pattern(/^[a-zA-Z0-9_]+$/)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8)]],
    confirmPassword: ['', [Validators.required]],
    agreeTerms: [false, [Validators.requiredTrue]]
  }, {
    validators: this.passwordMatchValidator
  });

  passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    if (password && confirmPassword && password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    }

    return null;
  }

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  toggleConfirmPassword(): void {
    this.showConfirmPassword = !this.showConfirmPassword;
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      const registerModel : RegistrationRequest = {
        email: this.registerForm.controls['email'].value,
        userName: this.registerForm.controls['userName'].value,
        firstName: this.registerForm.controls['firstName'].value,
        lastName: this.registerForm.controls['lastName'].value,
        password: this.registerForm.controls['password'].value,
        confirmPassword: this.registerForm.controls['confirmPassword'].value
      };
      this.authService.register(registerModel).subscribe({
        next(response) {
          console.log("Registration successful"); //тут перенаправление должно быть
        },
        error(error: HttpErrorResponse) {
          console.log(error); // тут нужно показать причину ошибки
        } 
      });
    } else {
      this.registerForm.markAllAsTouched();
    }
  }

  getPasswordStrength(): { label: string; class: string; width: string } {
    const password = this.registerForm.get('password')?.value || '';

    if (password.length === 0) {
      return { label: '', class: '', width: '0%' };
    }

    let strength = 0;
    if (password.length >= 8) strength++;
    if (password.length >= 12) strength++;
    if (/[A-Z]/.test(password)) strength++;
    if (/[0-9]/.test(password)) strength++;
    if (/[^A-Za-z0-9]/.test(password)) strength++;

    if (strength <= 2) {
      return { label: 'Слабый', class: 'weak', width: '33%' };
    } else if (strength <= 3) {
      return { label: 'Средний', class: 'medium', width: '66%' };
    } else {
      return { label: 'Сильный', class: 'strong', width: '100%' };
    }
  }
}
