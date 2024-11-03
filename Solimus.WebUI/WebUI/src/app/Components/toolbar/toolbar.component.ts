import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ThemeTogglerComponent } from "../theme-toggler/theme-toggler.component";
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { UserAvatarComponent } from "../user-avatar/user-avatar.component";

@Component({
  selector: 'app-toolbar',
  standalone: true,
  imports: [ThemeTogglerComponent, CommonModule, RouterLink, UserAvatarComponent],
  templateUrl: './toolbar.component.html',
  styleUrl: './toolbar.component.scss'
})
export class ToolbarComponent {  
  @Input() isLoggedIn = true;  
}
