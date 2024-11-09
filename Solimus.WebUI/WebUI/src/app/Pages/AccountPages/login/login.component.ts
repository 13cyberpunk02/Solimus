import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEye, faEyeSlash, IconDefinition } from '@fortawesome/free-solid-svg-icons'
import PasswordValidator from '../../../Shared/Validators/password-validator';
import { AuthService } from '../../../Services/CommonServices/Authentication/auth.service';
import { LoginRequest } from '../../../Models/Requests/Authentication/login';
import { MainErrorResponse } from '../../../Models/Reponse/mainErrorResponse';
import { MainSuccessResponse } from '../../../Models/Reponse/mainSuccessReponse';
import { ToastService, ToastType } from '../../../Services/CommonServices/Toast/toast.service';
import { HttpErrorResponse } from '@angular/common/http';
import { RegisterRequest } from '../../../Models/Requests/Authentication/register';
import { RefreshTokenRequest } from '../../../Models/Requests/Authentication/refreshToken';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [    
    CommonModule,
    FontAwesomeModule,    
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  loginForm!: FormGroup;
  registerForm!: FormGroup;    
  router = inject(Router);
  selectedTab: string = 'login';
  loginPasswordVisible: boolean = false;
  loginPasswordVisibleIcon: IconDefinition = faEye;

  registerPasswordVisible: boolean = false;
  registerPasswordVisibleIcon: IconDefinition = faEye;

  confirmPasswordVisible: boolean = false;
  confirmPasswordVisibleIcon: IconDefinition = faEye;

  constructor(
    private loginFb: FormBuilder,
    private registerFb: FormBuilder,
    private authService: AuthService,
    private toast: ToastService) {
    this.loginForm = this.loginFb.group({
      email: ['', [Validators.required, Validators.email]],
      loginPassword: ['', Validators.required]
    });

    this.registerForm = this.registerFb.group({
      email: ['', [Validators.required, Validators.email]],
      user_name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
      password: ['', [Validators.required, Validators.minLength(6), PasswordValidator.passwordStrength]],
      confirmPassword: ['', [Validators.required, PasswordValidator.matchPassword]]
    });
  }

  onLoginSubmit() {
    if (this.loginForm.valid) {
      const loginModel: LoginRequest = {
        email: this.loginForm.get('email')?.value,
        password: this.loginForm.get('loginPassword')?.value
      }
      if (loginModel) {
        this.authService.login(loginModel).subscribe({
          next: () => this.toast.show('Успешная авторизация', ToastType.Success),
          error: (response: HttpErrorResponse) => {
            this.toast.show(response.error.error.message, ToastType.Error);
          }
        });
      }
    } else {
      this.toast.show("Что то заполнено неправильно, проверьте", ToastType.Warning);
    }
  }

  onRegisterSubmit() {
    if (this.registerForm.valid) {  
      const registerModel: RegisterRequest = {
        email: this.registerForm.get('email')?.value,
        username: this.registerForm.get('user_name')?.value,
        password: this.registerForm.get('password')?.value,
        confirmPassword: this.registerForm.get('confirmPassword')?.value
      };
      this.authService.register(registerModel).subscribe({
        next: (response: MainErrorResponse | MainSuccessResponse) => {
          const message = response as MainSuccessResponse;
          this.toast.show(message.value, ToastType.Success);
          this.router.navigate(['/']);
        },
        error: (response: HttpErrorResponse) => {
          this.toast.show(response.error.error.message, ToastType.Error);
        }
      });
    }
  }

  selectTab(tab: string) {
    this.selectedTab = tab;
  }

  forgotPassword() {
    this.router.navigate(['/forgot-password']);
  }

  toggleLoginPasswordVisibility() {
    this.loginPasswordVisible = !this.loginPasswordVisible;
    this.loginPasswordVisibleIcon = this.loginPasswordVisible ? faEyeSlash : faEye;
  }

  toggleRegisterPasswordVisibility() {
    this.registerPasswordVisible = !this.registerPasswordVisible;
    this.registerPasswordVisibleIcon = this.registerPasswordVisible ? faEyeSlash : faEye;
  }

  toggleConfirmPasswordVisibility() {
    this.confirmPasswordVisible = !this.confirmPasswordVisible;
    this.confirmPasswordVisibleIcon = this.confirmPasswordVisible ? faEyeSlash : faEye;
  }
}