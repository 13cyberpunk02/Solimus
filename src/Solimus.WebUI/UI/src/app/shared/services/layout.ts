import { computed, Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LayoutService {
  private readonly STORAGE_KEY = 'sidebar-collapsed';

  readonly sidebarCollapsed = signal<boolean>(this.getInitialState());

  readonly mobileMenuOpen = signal<boolean>(false);

  readonly sidebarWidth = computed(() =>
    this.sidebarCollapsed() ? '72px' : '260px'
  );

  readonly mobileSidebarWidth = computed(() =>
    this.mobileMenuOpen() ? '260px' : '0px'
  );

  toggleSidebar(): void {
    this.sidebarCollapsed.update(state => {
      const newState = !state;
      localStorage.setItem(this.STORAGE_KEY, JSON.stringify(newState));
      return newState;
    });
  }

  collapseSidebar(): void {
    this.sidebarCollapsed.set(true);
    localStorage.setItem(this.STORAGE_KEY, 'true');
  }

  expandSidebar(): void {
    this.sidebarCollapsed.set(false);
    localStorage.setItem(this.STORAGE_KEY, 'false');
  }

  toggleMobileMenu(): void {
    this.mobileMenuOpen.update(state => !state);
  }

  closeMobileMenu(): void {
    this.mobileMenuOpen.set(false);
  }

  openMobileMenu(): void {
    this.mobileMenuOpen.set(true);
  }

  private getInitialState(): boolean {
    const stored = localStorage.getItem(this.STORAGE_KEY);
    if (stored !== null) {
      return JSON.parse(stored);
    }
    return false;
  }
}
