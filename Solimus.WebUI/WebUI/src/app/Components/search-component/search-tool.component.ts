import { CommonModule } from '@angular/common';
import { Component, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Channel } from '../../Models/Requests/Channel/channel';

@Component({
  selector: 'app-search-tool',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './search-tool.component.html',
  styleUrl: './search-tool.component.scss'
})
export class SearchToolComponent {
  @Input() items!: Channel[];
  @Input() findItemName!: string;
  @Output() item!: string;
  searchItem: string = '';

  get findItem() {
    return this.items.filter(item => {
      item.Name.includes(this.searchItem.toLocaleLowerCase());
      console.log(item);
    });
  }
}
