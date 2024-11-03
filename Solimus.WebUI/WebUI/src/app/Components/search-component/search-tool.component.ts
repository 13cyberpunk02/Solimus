import { CommonModule } from '@angular/common';
import { Component, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search-tool',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './search-tool.component.html',
  styleUrl: './search-tool.component.scss'
})
export class SearchToolComponent {
  @Input() items!: string[];
  @Input() findItemName!: string;
  @Output() item!: string;
  searchItem: string = '';

  get findItem() {
    return this.items.filter(item => {
      item.toLowerCase().includes(this.searchItem.toLocaleLowerCase());
      console.log(this.searchItem);
    });
  }
}
