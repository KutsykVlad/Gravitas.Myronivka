import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class AlertService {
  constructor(private snackBar: MatSnackBar) {}

  error(message: string) {
    this.snackBar.open(message, 'Закрити', {
      duration: 10000,
      panelClass: ['error']
    });
  }

  success(message: string) {
    this.snackBar.open(message, 'Закрити', {
      duration: 10000,
      panelClass: ['success']
    });
  }
}
