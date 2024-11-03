import { Routes } from '@angular/router';
import { ForgotPasswordComponent } from './Pages/AccountPages/forgot-password/forgot-password.component';
import { HomePageComponent } from './Pages/CommonPages/home-page/home-page.component';
import { ConfirmEmailComponent } from './Pages/AccountPages/confirm-mail/confirm-email.component';
import { ResetPasswordComponent } from './Pages/AccountPages/reset-password/reset-password.component';
import { CategoryPageComponent } from './Pages/TVPages/CategoryPage/category-page.component';
import { authGuard } from './Guards/AuthGuard/auth.guard';
import { UserEditComponent } from './Pages/AdminPage/UserEditPage/user-edit.component';
import { roleGuard } from './Guards/RoleGuard/role.guard';
import { MainPageComponent } from './Pages/TVPages/MainPage/main-page.component';
import { NotFounPageComponent } from './Pages/CommonPages/not-found/not-foun-page.component';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },    
    { path: 'home', component: HomePageComponent },    
    { path: 'forgot-password', component: ForgotPasswordComponent },
    { path: 'confirm-email', component: ConfirmEmailComponent },
    { path: 'reset-password', component: ResetPasswordComponent },
    { path: 'cagetory-edit', component: CategoryPageComponent, canActivate: [authGuard] },
    { path: 'user-edit', component: UserEditComponent, canActivate: [roleGuard], data: { roles: ['Администратор'] } },
    { path: 'main-page', component: MainPageComponent, canActivate: [authGuard] },
    { path: '**', component: NotFounPageComponent }
];
