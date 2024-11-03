import { Component } from '@angular/core';
import { SolimusUser } from '../../Models/Dto/solimus-user';
import { ClickOutsideDirective } from '../../Shared/Directives/click-outside.directive';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faUser, faRightFromBracket, faGear} from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-user-avatar',
  standalone: true,
  imports: [ClickOutsideDirective, CommonModule, FontAwesomeModule],
  templateUrl: './user-avatar.component.html',
  styleUrl: './user-avatar.component.scss'
})
export class UserAvatarComponent {
  
  userIcon = faUser;
  userSettings = faGear;
  logoutIcon = faRightFromBracket;

  user: SolimusUser = {
    email: 'salawat',
    id: "11",
    username: "cyber",
    firstName: "Salavat",
    lastName: "Sabirov"
  };
  
  isAvatarDropdownOpen = false;

  toggleDropdown() {
    this.isAvatarDropdownOpen = !this.isAvatarDropdownOpen;
  }

  closeDropdown() {
    this.isAvatarDropdownOpen = false;
  }
}
