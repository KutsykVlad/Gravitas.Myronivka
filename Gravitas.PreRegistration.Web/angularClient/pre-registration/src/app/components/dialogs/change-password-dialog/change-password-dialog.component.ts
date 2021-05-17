import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { SpinnerService } from 'src/app/services/spinner.service';
import { AuthService } from 'src/app/services/auth.service';
import { InfoDialogComponent } from '../info-dialog/info-dialog.component';

@Component({
  selector: 'app-change-password-dialog',
  templateUrl: './change-password-dialog.component.html',
  styleUrls: ['./change-password-dialog.component.scss']
})
export class ChangePasswordDialogComponent implements OnInit {
  public changePasswordForm: any;

  constructor(
    public dialogRef: MatDialogRef<ChangePasswordDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialog: MatDialog,
    private spinnerService: SpinnerService,
    public authService: AuthService
  ) {}

  ngOnInit() {
    this.changePasswordForm = new FormGroup({
      oldPassword: new FormControl('', Validators.required),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(2)
      ]),
      confirmPassword: new FormControl('', Validators.required)
    });
  }

  public close(): void {
    this.dialogRef.close();
  }

  public changePasssword() {
    if (this.changePasswordForm.valid) {
      this.spinnerService.showSpinner();
      this.authService
        .changePassword({
          oldPassword: this.changePasswordForm.controls.oldPassword.value,
          newPassword: this.changePasswordForm.controls.password.value,
          confirmPassword: this.changePasswordForm.controls.confirmPassword
            .value
        })
        .subscribe(
          result => {
            this.spinnerService.hideSpinner();
            this.openDialog({
              title: '',
              content: 'Пароль змінено'
            });
          },
          errorResult => {
            if (errorResult.status === 400) {
              let errorMessage = '';
              for (const property in errorResult.error.modelState) {
                if (errorResult.error.modelState.hasOwnProperty(property)) {
                  errorMessage += errorResult.error.modelState[property] + ' ';
                }
              }
              this.openDialog({
                title: 'Помилка',
                content: errorMessage
              });
            } else {
              this.openDialog({
                title: 'Помилка',
                content:
                  'Внутрішня помилка сервера, будь ласка спробуйте пізніше.'
              });
            }
            this.spinnerService.hideSpinner();
          }
        );
      this.close();
    }
  }

  private openDialog(dialogData: any): void {
    this.dialog.open(InfoDialogComponent, {
      width: '350px',
      data: dialogData
    });
  }
}
