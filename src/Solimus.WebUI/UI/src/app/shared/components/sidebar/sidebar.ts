import { Component, inject } from '@angular/core';
import { TuiIcon } from '@taiga-ui/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LayoutService } from '../../services/layout';
import { NavGroup } from '../../models/sidebar.model';
import { AuthService } from '@core/index';

@Component({
  selector: 'app-sidebar',
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    TuiIcon
  ],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css',
})
export class Sidebar {
  private readonly layoutService = inject(LayoutService);
  private readonly authService = inject(AuthService);

  readonly collapsed = this.layoutService.sidebarCollapsed;
  readonly mobileOpen = this.layoutService.mobileMenuOpen;
  readonly currentUser = this.authService.currentUser;

  readonly navGroups: NavGroup[] = [
    {
      items: [
        { label: 'Главная', route: '/home', icon: '@tui.home' },
        { label: 'Аналитика', route: '/analytics', icon: '@tui.chart-pie', badge: 'New' },
        { label: 'Проекты', route: '/projects', icon: '@tui.folder', badge: 5 },
      ]
    },
    {
      title: 'Управление',
      items: [
        { label: 'Пользователи', route: '/users', icon: '@tui.users' },
        { label: 'Команды', route: '/teams', icon: '@tui.user-check' },
        { label: 'Роли', route: '/roles', icon: '@tui.shield' },
      ]
    },
    {
      title: 'Контент',
      items: [
        { label: 'Документы', route: '/documents', icon: '@tui.file-text' },
        { label: 'Медиа', route: '/media', icon: '@tui.image' },
        { label: 'Сообщения', route: '/messages', icon: '@tui.mail', badge: 12 },
      ]
    },
    {
      title: 'Система',
      items: [
        { label: 'Настройки', route: '/settings', icon: '@tui.settings' },
        { label: 'Помощь', route: '/help', icon: '@tui.help-circle' },
      ]
    }
  ];

  closeMobileMenu(): void {
    this.layoutService.closeMobileMenu();
  }

  onNavItemClick(): void {
    // Close mobile menu on navigation
    if (window.innerWidth < 1024) {
      this.closeMobileMenu();
    }
  }

  logout(): void {
    this.authService.logout();
  }
}
