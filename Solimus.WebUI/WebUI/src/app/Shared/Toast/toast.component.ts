import { Component, OnInit } from '@angular/core';
import { ToastService, ToastType } from '../../Services/CommonServices/Toast/toast.service';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCheckCircle, faTriangleExclamation, faBomb, faCircleInfo, faHandFist } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule, FontAwesomeModule],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.scss'
})
export class ToastComponent implements OnInit {
  toast: { message: string, type: ToastType } | null = null; 
  
  constructor(private toastService: ToastService) { }

  ngOnInit() {
    this.toastService.toast$.subscribe(toast => this.toast = toast);
  }

  getToastType() {
    switch (this.toast!.type) {
      case ToastType.Success:
        return 'bg-green-500 shadow-green-800';
      case ToastType.Error:
        return 'bg-red-500 shadow-red-800';
      case ToastType.Info:
        return 'bg-blue-500 shadow-blue-800';
      case ToastType.Warning:
        return 'bg-orange-500 shadow-orange-800';
      default:
        return 'bg-violet-500 shadow-violet-800';
    }
  }

  getToastIcon() {
    switch (this.toast!.type) {
      case ToastType.Success:
        return faCheckCircle;
      case ToastType.Error:
        return faBomb;
      case ToastType.Info:
        return faCircleInfo;
      case ToastType.Warning:
        return faTriangleExclamation;
      default:
        return faHandFist;
    }
  }
}
