import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ToastService, ToastType } from '../../Services/CommonServices/Toast/toast.service';
import { AuthService } from '../../Services/CommonServices/Authentication/auth.service';

export const authGuard: CanActivateFn = (state) => {
  const router = inject(Router);
  const toast = inject(ToastService);
  const authService = inject(AuthService);

  if (!authService.isLoggedIn()) {
    toast.show("Вы не авторизованы, у вас нет доступа для просмотра данной страницы", ToastType.Warning);
    router.navigate(['/home'], { queryParams: { returnUrl: state.url } });
    return false;
  }
  return true;
};
