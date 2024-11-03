import { CommonModule, NgClass } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEye, faEyeSlash, IconDefinition } from '@fortawesome/free-solid-svg-icons'
import { PasswordInputComponent } from "../../../Components/password-input/password-input/password-input.component";
import PasswordValidator from '../../../Shared/Validators/password-validator';
import { AuthService } from '../../../Services/CommonServices/Authentication/auth.service';
import { RegisterRequest } from '../../../Models/Requests/Authentication/register';
import { LoginRequest } from '../../../Models/Requests/Authentication/login';
import { MainErrorResponse } from '../../../Models/Reponse/mainErrorResponse';
import { MainSuccessResponse } from '../../../Models/Reponse/mainSuccessReponse';
import { ToastService, ToastType } from '../../../Services/CommonServices/Toast/toast.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    NgClass,
    CommonModule,
    FontAwesomeModule,
    PasswordInputComponent,
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  loginForm!: FormGroup;
  registerForm!: FormGroup;
  loginModel: LoginRequest = { email: '', password: '' };
  registerModel: RegisterRequest = { email: '', password: '', username: '' };
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
          next: (response: MainErrorResponse | MainSuccessResponse) => {
            const result = response as MainSuccessResponse;
            this.authService.saveToken(result.value);
            this.toast.show('Успешная авторизация', ToastType.Success);
          },
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
      this.registerModel.email = this.registerForm.get('email')?.value;
      this.registerModel.password = this.registerForm.get('password')?.value;
      this.registerModel.username = this.registerForm.get('user_name')?.value;
      this.authService.register(this.registerModel).subscribe({
        next: (response: MainErrorResponse | MainSuccessResponse) => {
          const message = response as MainSuccessResponse;
          this.toast.show(message.value, ToastType.Success);
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