import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../Services/CommonServices/Authentication/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmEmailRequest } from '../../../Models/Requests/Authentication/confirmEmail';
import { MainSuccessResponse } from '../../../Models/Reponse/mainSuccessReponse';
import { MainErrorResponse } from '../../../Models/Reponse/mainErrorResponse';
import { ToastService, ToastType } from '../../../Services/CommonServices/Toast/toast.service';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faRightToBracket, faEnvelope } from '@fortawesome/free-solid-svg-icons'
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-confirm-email',
  standalone: true,
  imports: [CommonModule, FontAwesomeModule, FormsModule],
  templateUrl: './confirm-email.component.html',
  styleUrl: './confirm-email.component.scss'
})
export class ConfirmEmailComponent implements OnInit {

  confirmed: boolean = false;
  loginIcon = faRightToBracket;
  mailIcon = faEnvelope;
  email: string = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private toast: ToastService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.queryParamMap.subscribe({
      next: (params: any) => {
        const confirmEmail: ConfirmEmailRequest = {
          email: params.get('email'),
          token: params.get('token')
        };
        if (confirmEmail) {
          this.authService.confirmEmail(confirmEmail).subscribe({
            next: (response: MainErrorResponse | MainSuccessResponse) => {
              if (this.isSuccess(response)) {
                this.toast.show(response.value, ToastType.Success);
                this.confirmed = true;
              }
            },
            error: (response: HttpErrorResponse) => {
              this.toast.show(response.error.error.message, ToastType.Warning);
              this.router.navigate(['/home'])
            }
          });
        } else {
          this.toast.show("Электронная почта или токен был выдан неправильно", ToastType.Info);
          this.router.navigate(['/home']);
        }
      },
      error: _ => {
        this.toast.show('Не было отправлено эл. почта и токен для подтверждения.', ToastType.Error);
      }
    });
  }

  private isSuccess(model: any): model is MainSuccessResponse {
    return (model as MainSuccessResponse).value !== undefined;
  }

  login() {
    this.router.navigate(['/home']);
  }

  resendConfirmEmail() {

  }

}
