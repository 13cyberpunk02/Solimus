import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

interface Toast {
  message: string;
  type: ToastType;
}

export enum ToastType {
  Success = 'success',
  Error = 'error',
  Info = 'info',
  Warning = 'warning'
}

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private toastSubject = new BehaviorSubject<Toast | null>(null);
  toast$: Observable<Toast | null> = this.toastSubject.asObservable();

  show(message: string, type: ToastType = ToastType.Info) {
    this.toastSubject.next({message, type});
    setTimeout(() => this.toastSubject.next(null), 3000);
  }
  
}
