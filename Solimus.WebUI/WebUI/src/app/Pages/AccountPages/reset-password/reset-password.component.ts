import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEye, faEyeSlash, faBolt, IconDefinition } from '@fortawesome/free-solid-svg-icons'
import { AuthService } from '../../../Services/CommonServices/Authentication/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService, ToastType } from '../../../Services/CommonServices/Toast/toast.service';
import { HttpErrorResponse } from '@angular/common/http';
import PasswordValidator from '../../../Shared/Validators/password-validator';
import { ResetPasswordRequest } from '../../../Models/Requests/Authentication/resetPassword';
import { MainErrorResponse } from '../../../Models/Reponse/mainErrorResponse';
import { MainSuccessResponse } from '../../../Models/Reponse/mainSuccessReponse';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [FontAwesomeModule, CommonModule, ReactiveFormsModule],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss'
})
export class ResetPasswordComponent {

  resetPasswordForm!: FormGroup;
  resetPasswordVisible: boolean = false;
  confirmPasswordVisible: boolean = false;
  resetPasswordVisibleIcon: IconDefinition = faEye;
  confirmPasswordVisibleIcon: IconDefinition = faEye;
  resetPasswordModel: ResetPasswordRequest = { email: '', newPassword: '', token: '' };
  changeIcon = faBolt;

  constructor(
    private authService: AuthService,
    private router: Router,
    private toast: ToastService,
    private activatedRoute: ActivatedRoute,
    private resetPasswordFb: FormBuilder,
  ) {
    this.resetPasswordForm = this.resetPasswordFb.group({
      password: ['', [Validators.required, Validators.minLength(6), PasswordValidator.passwordStrength]],
      confirmPassword: ['', [Validators.required, PasswordValidator.matchPassword]]
    });
    this.activatedRoute.queryParamMap.subscribe({
      next: (params: any) => {
        this.resetPasswordModel.email = params.get('email');
        this.resetPasswordModel.token = params.get('token');
      },
      error: (response: HttpErrorResponse) => {
        if (response.error) {
          this.toast.show(response.error.error, ToastType.Error);
          this.router.navigate(['/home']);
        }
      }
    });
  }

  private resetPassword() {
    this.authService.resetPassword(this.resetPasswordModel).subscribe({
      next: (response: MainErrorResponse | MainSuccessResponse) => {
        const message = response as MainSuccessResponse;
        this.toast.show(message.value, ToastType.Success);
        this.router.navigate(['/home']);
      },
      error: (response: HttpErrorResponse) => {
        this.toast.show(response.error.detail, ToastType.Error);
        this.router.navigate(['/home']);
      }
    });
  }

  onSubmit() {
    if (this.resetPasswordForm.valid) {
      this.resetPasswordModel.newPassword = this.resetPasswordForm.get('password')?.value;
      this.resetPassword();
    } else {
      this.toast.show("Проверьте правильность набора пароля", ToastType.Warning);
    }
  }

  toggleResetPasswordVisibility() {
    this.resetPasswordVisible = !this.resetPasswordVisible;
    this.resetPasswordVisibleIcon = this.resetPasswordVisible ? faEyeSlash : faEye;
  }

  toggleConfirmPasswordVisibility() {
    this.confirmPasswordVisible = !this.confirmPasswordVisible;
    this.confirmPasswordVisibleIcon = this.confirmPasswordVisible ? faEyeSlash : faEye;
  }
}