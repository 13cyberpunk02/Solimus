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

  ngOnInit(): void {
    this.channels.push({
      Id: "1",
      Image: "http://epg.one/img/5212.png",
      Name: "MM Ужасы HD",
      Source: "http://14rfkfew.otttv.pw/iptv/6EDNE7ZGASZMBQ/13099/index.m3u8"
    });
    this.channels.push({
      Id: "2",
      Image: "http://epg.one/img/1116.png",
      Name: "Z! Horror HD",
      Source: "http://14rfkfew.otttv.pw/iptv/6EDNE7ZGASZMBQ/14137/index.m3u8"
    });
    this.channels.push({
      Id: "3",
      Image: "http://epg.one/img/5514.png",
      Name: "BCU Marvel HD",
      Source: "http://14rfkfew.otttv.pw/iptv/6EDNE7ZGASZMBQ/15025/index.m3u8"
    });
    this.channels.push({
      Id: "4",
      Image: "http://epg.one/img/6236.png",
      Name: "KLI Fantastic HD",
      Source: "http://14rfkfew.otttv.pw/iptv/6EDNE7ZGASZMBQ/18050/index.m3u8"
    });
    this.channels.push({
      Id: "5",
      Image: "http://epg.one/img/680.png",
      Name: "ТНТ Music HD",
      Source: "http://14rfkfew.otttv.pw/iptv/6EDNE7ZGASZMBQ/19223/index.m3u8"
    });
    this.channels.push({
      Id: "6",
      Image: "http://epg.one/img/4474.png",
      Name: "National Geographic",
      Source: "http://14rfkfew.otttv.pw/iptv/6EDNE7ZGASZMBQ/14001/index.m3u8"
    });   
  }
}
