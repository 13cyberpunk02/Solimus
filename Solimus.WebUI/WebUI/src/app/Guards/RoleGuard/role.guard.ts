import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../Services/CommonServices/Authentication/auth.service';
import { ToastService, ToastType } from '../../Services/CommonServices/Toast/toast.service';

export const roleGuard: CanActivateFn = (route, state) => {
  const roles = route.data["roles"] as string[];
  const authService = inject(AuthService);
  const toast = inject(ToastService);
  const router = inject(Router);

  const userRoles = authService.getCurrentUserRoles();

  if (!authService.isLoggedIn()) {
    router.navigate(['/home']);
    toast.show("Вы не авторизованы", ToastType.Info);
    return false;
  }

  if(roles.some((role) => userRoles?.includes(role))) return true;

  router.navigate(['/home']);
  toast.show("У вас недостаточно прав для просмотра данной страницы", ToastType.Warning);
  return false;
};
