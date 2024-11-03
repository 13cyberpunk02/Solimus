import { Component, EventEmitter, Input, Output } from '@angular/core';
import { faEye, faEyeSlash, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormControl, ReactiveFormsModule,  } from '@angular/forms';

@Component({
  selector: 'app-password-input',
  standalone: true,
  imports: [
    FontAwesomeModule, ReactiveFormsModule
  ],
  templateUrl: './password-input.component.html',
  styleUrl: './password-input.component.scss'
})
export class PasswordInputComponent {
  @Input() placeholder: string = 'Введите пароль';
  @Output() passwordChange: EventEmitter<string> = new EventEmitter<string>();
  
  passwordControl = new FormControl('');
  passwordVisible: boolean = false;
  passwordVisibleIcon: IconDefinition = faEye;

  constructor() {
    this.passwordControl.valueChanges.subscribe(value => {
      if(value) this.passwordChange.emit(value);
    });
  }

  togglePasswordVisibility() {
    this.passwordVisible = !this.passwordVisible;
    this.passwordVisibleIcon = this.passwordVisible ? faEyeSlash : faEye;
  }
}
