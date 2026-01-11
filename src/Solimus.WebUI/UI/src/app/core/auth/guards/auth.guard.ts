import { inject } from '@angular/core';
import { Router, CanActivateFn, CanMatchFn } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.hasValidToken()) {
    return true;
  }

  router.navigate(['/login'], {
    queryParams: { returnUrl: state.url }
  });

  return false;
};

export const authMatchGuard: CanMatchFn = (route, segments) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.hasValidToken()) {
    return true;
  }

  router.navigate(['/login']);
  return false;
};

export const guestGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (!authService.hasValidToken()) {
    return true;
  }

  router.navigate(['/home']);
  return false;
};

export const roleGuard = (allowedRoles: string[]): CanActivateFn => {
  return (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    const user = authService.currentUser();

    if (!user) {
      router.navigate(['/login']);
      return false;
    }

    const userRoles = Array.isArray(user.role) ? user.role : [user.role];
    const hasRole = allowedRoles.some(role => userRoles.includes(role));

    if (hasRole) {
      return true;
    }

    router.navigate(['/forbidden']);
    return false;
  };
};
