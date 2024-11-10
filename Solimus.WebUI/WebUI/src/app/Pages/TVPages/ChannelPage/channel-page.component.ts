import { Component, OnInit } from '@angular/core';
import { SearchToolComponent } from "../../../Components/search-component/search-tool.component";
import { Channel } from '../../../Models/Requests/Channel/channel';
import { RectangleComponent } from "../../../Components/rectangle/rectangle.component";


@Component({
  selector: 'app-channel-page',
  standalone: true,
  imports: [SearchToolComponent, RectangleComponent],
  templateUrl: './channel-page.component.html',
  styleUrl: './channel-page.component.scss'
})
export class ChannelPageComponent implements OnInit {
  
  findItemName = 'канала';
  channels: Channel[] = [];
  items: string[] = ['image1.jpg', 'image2.jpg', 'image3.jpg'];

  ngOnInit(): void {
    this.channels.push({
      Id: "1",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "2",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "3",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "4",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "5",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "6",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "7",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "8",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "9",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "10",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "11",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "12",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "13",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "14",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });
    this.channels.push({
      Id: "15",
      Image: "dwd",
      Name: "Первый",
      Source: "wdwqdwd"
    });    
  }
}
