import { Component, OnInit} from '@angular/core';
import { Category } from '../../../Models/Requests/Category/category';
import { CommonModule } from '@angular/common';
import { RectangleComponent } from "../../../Components/rectangle/rectangle.component";



@Component({
  selector: 'app-category-page',
  standalone: true,
  imports: [CommonModule, RectangleComponent ],
  templateUrl: './category-page.component.html',
  styleUrl: './category-page.component.scss'
})
export class CategoryPageComponent implements OnInit {
  categories: Category[] = [];


  scrollRight() {
    const container = document.querySelector('.flex.overflow-x-auto') as HTMLElement;
    container.scrollBy({ left: 200, behavior: 'smooth' });
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
  }
}
