import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-rectangle',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './rectangle.component.html',
  styleUrl: './rectangle.component.scss'
})
export class RectangleComponent {
  @Input() title: string = '';
  @Input() imageUrl: string = '';
  borderClasses: string[] = ['border-red', 'border-blue', 'border-yellow'];

  currentBorderColor: string = ''; // Инициализируем как пустую строку

    // Метод для смены цвета рамки
    changeBorderColor() {
      const randomIndex = Math.floor(Math.random() * this.borderClasses.length);
      this.currentBorderColor = this.borderClasses[randomIndex]; // Устанавливаем случайный цвет рамки
    }

      // Метод для обработки ухода курсора
  onMouseLeave() {
    this.currentBorderColor = ''; // Сбрасываем цвет рамки
  }
  handleClick() {
    alert(`Вы нажали на ${this.title}`);
  }
}
