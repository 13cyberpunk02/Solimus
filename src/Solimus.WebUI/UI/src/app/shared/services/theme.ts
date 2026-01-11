import { effect, Injectable, signal } from '@angular/core';

export type ThemeMode = 'light' | 'dark';

@Injectable({
  providedIn: 'root',
})
export class Theme {
  private readonly STORAGE_KEY = 'app-theme';

  readonly theme = signal<ThemeMode>(this.getInitialTheme());
  readonly isDark = signal<boolean>(this.theme() === 'dark');

  constructor() {
    effect(() => {
      const currentTheme = this.theme();
      this.isDark.set(currentTheme === 'dark');
      this.applyTheme(currentTheme);
      localStorage.setItem(this.STORAGE_KEY, currentTheme);
    });
  }

  toggleTheme(): void {
    this.theme.update(current => current === 'light' ? 'dark' : 'light');
  }

  setTheme(theme: ThemeMode): void {
    this.theme.set(theme);
  }

  private getInitialTheme(): ThemeMode {
    const stored = localStorage.getItem(this.STORAGE_KEY) as ThemeMode | null;

    if (stored) {
      return stored;
    }

    const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    return prefersDark ? 'dark' : 'light';
  }

  private applyTheme(theme: ThemeMode): void {
    const root = document.documentElement;

    if (theme === 'dark') {
      root.classList.add('dark');
      root.setAttribute('data-theme', 'dark');
    } else {
      root.classList.remove('dark');
      root.setAttribute('data-theme', 'light');
    }
  }
}
