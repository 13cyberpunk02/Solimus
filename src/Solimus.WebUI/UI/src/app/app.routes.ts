import { Routes } from '@angular/router';
import { Home } from './features/home/index';
import { guestGuard } from './core/auth/guards/auth.guard';

export const routes: Routes =
    [
        {
            path: 'home',
            component: Home
        }, {
            path: 'login',
            loadComponent: () => import('./features/auth/components/login/login').then(m => m.Login),
            canActivate: [guestGuard]
        },
        {
            path: 'register',
            loadComponent: () => import('./features/auth/components/register/register').then(m => m.Register),
            canActivate: [guestGuard]
        },
        {
            path: '',
            redirectTo: '/home',
            pathMatch: 'full'
        }
    ];