import { Component } from '@angular/core';
import { SearchToolComponent } from "../../../Components/search-component/search-tool.component";


@Component({
  selector: 'app-channel-page',
  standalone: true,
  imports: [SearchToolComponent],
  templateUrl: './channel-page.component.html',
  styleUrl: './channel-page.component.scss'
})
export class ChannelPageComponent {
  findItemName = 'канала';
  items: string[] = ['image1.jpg', 'image2.jpg', 'image3.jpg'];
}
