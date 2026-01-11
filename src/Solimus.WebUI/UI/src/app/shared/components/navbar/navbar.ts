import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TuiButton, TuiIcon, TuiDropdownDirective, TuiDropdown } from '@taiga-ui/core';
import { Theme } from '../../services/theme';
import { LayoutService } from '../../services/layout';
import { AuthService } from '../../../core/auth/services/auth.service';

@Component({
  selector: 'app-navbar',
  imports: [
    CommonModule,
    RouterLink,
    TuiButton,
    TuiIcon,
    TuiDropdown
  ],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  private readonly themeService = inject(Theme);
  private readonly layoutService = inject(LayoutService);
  private readonly authService = inject(AuthService);

  readonly isDark = this.themeService.isDark;
  readonly sidebarCollapsed = this.layoutService.sidebarCollapsed;
  readonly mobileMenuOpen = this.layoutService.mobileMenuOpen;
  readonly isAuthenticated = this.authService.isAuthenticated;
  readonly currentUser = this.authService.currentUser;

  authDropdownOpen = false;
  userDropdownOpen = false;

  toggleTheme(): void {
    this.themeService.toggleTheme();
  }

  toggleSidebar(): void {
    this.layoutService.toggleSidebar();
  }

  toggleMobileMenu(): void {
    this.layoutService.toggleMobileMenu();
  }

  toggleAuthDropdown(): void {
    this.authDropdownOpen = !this.authDropdownOpen;
  }

  closeAuthDropdown(): void {
    this.authDropdownOpen = false;
  }

  toggleUserDropdown(): void {
    this.userDropdownOpen = !this.userDropdownOpen;
  }

  closeUserDropdown(): void {
    this.userDropdownOpen = false;
  }

  logout(): void {
    this.closeUserDropdown();
    this.authService.logout();
  }
}
