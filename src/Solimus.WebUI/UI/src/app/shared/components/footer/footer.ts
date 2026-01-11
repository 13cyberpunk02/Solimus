import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TuiIcon } from '@taiga-ui/core';

@Component({
  selector: 'app-footer',
  imports: [CommonModule, RouterLink, TuiIcon],
  templateUrl: './footer.html',
  styleUrl: './footer.css',
})
export class Footer {
  readonly currentYear = new Date().getFullYear();

  readonly socialLinks = [
    { icon: '@tui.github', url: 'https://github.com', label: 'GitHub' },
    { icon: '@tui.twitter', url: 'https://twitter.com', label: 'Twitter' },
    { icon: '@tui.linkedin', url: 'https://linkedin.com', label: 'LinkedIn' },
  ];

  readonly footerLinks = [
    { label: 'О нас', route: '/about' },
    { label: 'Контакты', route: '/contact' },
    { label: 'Политика конфиденциальности', route: '/privacy' },
    { label: 'Условия использования', route: '/terms' },
  ];
}
