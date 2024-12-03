import {Component, ElementRef, Renderer2, ViewChild} from '@angular/core';
import { SolimusUser } from '../../Models/Dto/solimus-user';
import { ClickOutsideDirective } from '../../Shared/Directives/click-outside.directive';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faUser, faRightFromBracket, faGear} from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-user-avatar',
  standalone: true,
  imports: [ClickOutsideDirective, FontAwesomeModule],
  templateUrl: './user-avatar.component.html',
  styleUrl: './user-avatar.component.scss'
})
export class UserAvatarComponent {

  userIcon = faUser;
  userSettings = faGear;
  logoutIcon = faRightFromBracket;
  isMenuOpen = false;

  user: SolimusUser = {
    email: 'salawat1302@gmail.ru',
    id: "11",
    username: "cyber",
    firstName: "Salavat",
    lastName: "Sabirov"
  };
  toggleDropdown() {
    this.isMenuOpen = !this.isMenuOpen;
  }
}
