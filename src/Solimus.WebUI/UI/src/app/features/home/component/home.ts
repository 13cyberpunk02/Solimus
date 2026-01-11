import { CommonModule } from '@angular/common';
import { AfterViewInit, Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TuiButton, TuiIcon } from '@taiga-ui/core';
import { BlogPost, TechCategory } from '../models/home.model';

declare const Prism: any;

@Component({
  selector: 'app-home',
  imports: [
    CommonModule,
    RouterLink,
    TuiButton,
    TuiIcon
  ],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements AfterViewInit {
  readonly codeExample = `import { Component, signal } from '@angular/core';

@Component({
  selector: 'app-root',
  template: \`<h1>{{ title() }}</h1>\`
})
export class AppComponent {
  title = signal('Hello World!');
}`;

  readonly categories: TechCategory[] = [
    {
      name: 'Angular',
      icon: '@tui.component',
      description: 'Современная разработка на Angular 21 с сигналами и standalone компонентами',
      postsCount: 12,
      gradient: 'linear-gradient(135deg, #dd0031 0%, #c3002f 100%)'
    },
    {
      name: '.NET',
      icon: '@tui.server',
      description: 'Backend разработка на .NET 9-10, Web API, микросервисы',
      postsCount: 8,
      gradient: 'linear-gradient(135deg, #512bd4 0%, #7b4dff 100%)'
    },
    {
      name: 'EF Core',
      icon: '@tui.database',
      description: 'Entity Framework Core, миграции, оптимизация запросов',
      postsCount: 6,
      gradient: 'linear-gradient(135deg, #68217a 0%, #9b4dca 100%)'
    },
    {
      name: 'Docker',
      icon: '@tui.box',
      description: 'Контейнеризация, Docker Compose, оркестрация',
      postsCount: 5,
      gradient: 'linear-gradient(135deg, #2496ed 0%, #0db7ed 100%)'
    },
    {
      name: 'PostgreSQL',
      icon: '@tui.hard-drive',
      description: 'Работа с PostgreSQL, индексы, производительность',
      postsCount: 4,
      gradient: 'linear-gradient(135deg, #336791 0%, #4a90b9 100%)'
    },
    {
      name: 'DevOps',
      icon: '@tui.git-branch',
      description: 'CI/CD, автоматизация, инфраструктура как код',
      postsCount: 3,
      gradient: 'linear-gradient(135deg, #f97316 0%, #fb923c 100%)'
    }
  ];

  readonly featuredPosts: BlogPost[] = [
    {
      id: 1,
      title: 'Сигналы в Angular 21: Полное руководство',
      excerpt: 'Разбираемся с новой реактивной системой Angular. Signals, computed, effect — всё что нужно знать для продуктивной работы.',
      category: 'Angular',
      categoryColor: '#dd0031',
      date: '5 янв 2026',
      readTime: '12 мин'
    },
    {
      id: 2,
      title: 'Minimal API в .NET 10: Что нового?',
      excerpt: 'Обзор новых возможностей Minimal API, улучшения производительности и новые паттерны разработки.',
      category: '.NET',
      categoryColor: '#512bd4',
      date: '3 янв 2026',
      readTime: '8 мин'
    },
    {
      id: 3,
      title: 'Docker Multi-stage Builds для .NET приложений',
      excerpt: 'Оптимизируем размер образов и время сборки с помощью multi-stage builds. Практические примеры и best practices.',
      category: 'Docker',
      categoryColor: '#2496ed',
      date: '1 янв 2026',
      readTime: '10 мин'
    },
    {
      id: 4,
      title: 'EF Core 9: Bulk Operations и производительность',
      excerpt: 'Новые возможности массовых операций в EF Core 9. Сравнение производительности и рекомендации.',
      category: 'EF Core',
      categoryColor: '#68217a',
      date: '28 дек 2025',
      readTime: '15 мин'
    }
  ];

  ngAfterViewInit(): void {
    if (typeof Prism !== 'undefined') {
      Prism.highlightAll();
    }
  }
}
