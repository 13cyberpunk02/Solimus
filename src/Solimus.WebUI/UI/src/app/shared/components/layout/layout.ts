import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { Navbar } from '../navbar/navbar';
import { Sidebar } from '../sidebar/sidebar';
import { Footer } from '../footer/footer';
import { filter, map, startWith } from 'rxjs/operators';
import { toSignal } from '@angular/core/rxjs-interop';
import { LayoutService } from '../../services/layout';
import { AuthService } from '../../../core/auth/services/auth.service';

@Component({
  selector: 'app-layout',
  imports: [
    CommonModule,
    RouterOutlet,
    Navbar,
    Sidebar,
    Footer
  ],
  templateUrl: './layout.html',
  styleUrl: './layout.css',
})
export class Layout {
  private readonly router = inject(Router);
  private readonly layoutService = inject(LayoutService);
  private readonly authService = inject(AuthService);

  readonly sidebarCollapsed = this.layoutService.sidebarCollapsed;
  readonly isAuthenticated = this.authService.isAuthenticated;

  readonly showFooter = toSignal(
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      map(() => this.router.url),
      startWith(this.router.url),
      map(url => url === '/' || url === '/home' || url.startsWith('/home?'))
    ),
    { initialValue: false }
  );
}
