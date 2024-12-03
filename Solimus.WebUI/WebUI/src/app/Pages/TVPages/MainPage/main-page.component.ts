import { Component } from '@angular/core';
import { ToolbarComponent } from "../../../Components/toolbar/toolbar.component";
import { CategoryPageComponent } from "../CategoryPage/category-page.component";
import { FooterComponent } from "../../../Components/footer/footer.component";
import { ChannelPageComponent } from "../ChannelPage/channel-page.component";

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [ToolbarComponent, CategoryPageComponent, ChannelPageComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent {
  isLoggedIn: boolean = true;
}
