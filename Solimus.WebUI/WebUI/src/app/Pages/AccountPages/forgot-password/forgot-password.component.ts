
import { Component } from '@angular/core';
import { FormBuilder, FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastService, ToastType } from '../../../Services/CommonServices/Toast/toast.service';
import { AuthService } from '../../../Services/CommonServices/Authentication/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MainSuccessResponse } from '../../../Models/Reponse/mainSuccessReponse';
import { MainErrorResponse } from '../../../Models/Reponse/mainErrorResponse';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss'
})
export class ForgotPasswordComponent {
  email: string = '';

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private toast: ToastService,
    private authService: AuthService) { }

  onSubmit() {
    if (this.email) {
      this.authService.forgotPassword(this.email).subscribe({
        next: (response: MainSuccessResponse | MainErrorResponse) => {
          var r = response as MainSuccessResponse;
          this.toast.show(r.value, ToastType.Info);
          this.router.navigate(['/home']);
        },
        error: (_response: HttpErrorResponse) => {
          this.toast.show("Что то пошло не так", ToastType.Warning);
        }
      });
    }
  }
}

