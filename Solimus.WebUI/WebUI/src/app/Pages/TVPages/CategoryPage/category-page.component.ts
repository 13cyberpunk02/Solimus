import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Category } from '../../../Models/Requests/Category/category';
import { CommonModule } from '@angular/common';
import { RectangleComponent } from "../../../Components/rectangle/rectangle.component";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faArrowAltCircleLeft, faArrowAltCircleRight } from '@fortawesome/free-solid-svg-icons'
@Component({
  selector: 'app-category-page',
  standalone: true,
  imports: [CommonModule, RectangleComponent, FontAwesomeModule],
  templateUrl: './category-page.component.html',
  styleUrl: './category-page.component.scss'
})
export class CategoryPageComponent implements OnInit {
  rightIcon = faArrowAltCircleRight;
  leftIcon = faArrowAltCircleLeft;
  categories: Category[] = [];

  @ViewChild('scrollContainer', { read: ElementRef }) scrollContainer!: ElementRef;

  scrollLeft() {
    this.scrollContainer.nativeElement.scrollBy({ left: -200, behavior: 'smooth' });
  }

  scrollRight() {
    this.scrollContainer.nativeElement.scrollBy({ left: 200, behavior: 'smooth' });
  }

  ngOnInit(): void {
    this.categories.push({
      Id: "1",
      Image: '/groups/theatre.png',
      Name: 'Фильмы'
    });
    this.categories.push({
      Id: "2",
      Image: '/groups/music.png',
      Name: 'Музыка'
    });
    this.categories.push({
      Id: "3",
      Image: '/groups/sports.png',
      Name: 'Спорт'
    });
    this.categories.push({
      Id: "4",
      Image: '/groups/episode.png',
      Name: 'Сериалы'
    });
    this.categories.push({
      Id: "5",
      Image: '/groups/news.png',
      Name: 'Новости'
    });
    this.categories.push({
      Id: "6",
      Image: '/groups/watching-a-movie.png',
      Name: 'Фильмы'
    });
    this.categories.push({
      Id: "7",
      Image: '/groups/globe.png',
      Name: 'Познавательные'
    });
    this.categories.push({
      Id: "8",
      Image: '/groups/adult.png',
      Name: '18+'
    });
    this.categories.push({
      Id: "9",
      Image: '/groups/adult.png',
      Name: '18+'
    });
    this.categories.push({
      Id: "10",
      Image: '/groups/adult.png',
      Name: '18+'
    });
  }
}
