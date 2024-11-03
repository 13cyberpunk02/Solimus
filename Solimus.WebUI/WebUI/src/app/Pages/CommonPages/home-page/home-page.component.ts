import { Component } from '@angular/core';
import { LoginComponent } from "../../AccountPages/login/login.component";
import { ToolbarComponent } from "../../../Components/toolbar/toolbar.component";
import { FooterComponent } from "../../../Components/footer/footer.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [LoginComponent, ToolbarComponent, FooterComponent, CommonModule ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {
  isLoggedIn: boolean = true;
  
  constructor() {
   // this.isLoggedIn = localStorage.getItem('token') ? true : false;
  }
}
