import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faInstagram, faVk, faTelegram } from '@fortawesome/free-brands-svg-icons'

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [FontAwesomeModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {
  instagram = faInstagram;
  vk = faVk;
  tg = faTelegram;
  date = new Date().getFullYear();
}
