import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ConfirmDialogComponent } from '../dialogs/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  constructor(
    private router: Router,
    public authService: AuthService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {}

  private logout() {
    this.authService.logout();
    this.router.navigateByUrl('trucks-info');
  }

  private openConfirmDialog(dialogData: any) {
    return this.dialog.open(ConfirmDialogComponent, {
      data: dialogData
    });
  }

  public confirmLogout() {
    const dialogRef = this.openConfirmDialog({
      title: 'Вийти із системи?',
      content: ''
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.logout();
      }
    });
  }
}
