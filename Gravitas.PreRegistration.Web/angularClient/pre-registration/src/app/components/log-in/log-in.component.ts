import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { SpinnerService } from 'src/app/services/spinner.service';
import { InfoDialogComponent } from '../dialogs/info-dialog/info-dialog.component';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.scss']
})
export class LogInComponent implements OnInit {
  public loginFormGroup: any;

  private EMAIL_REGEXP =
    // tslint:disable-next-line:max-line-length
    /^\s*(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))\s*$/;

  constructor(
    private router: Router,
    private authService: AuthService,
    public dialog: MatDialog,
    private spinnerService: SpinnerService
  ) {}

  ngOnInit() {
    this.loginFormGroup = new FormGroup({
      email: new FormControl('', [
        Validators.required,
        Validators.pattern(this.EMAIL_REGEXP)
      ]),
      password: new FormControl('', Validators.required)
    });
  }

  public login() {
    if (this.loginFormGroup.valid) {
      this.spinnerService.showSpinner();
      this.authService.login(this.loginFormGroup.value).subscribe(
        result => {
          this.spinnerService.hideSpinner();
          this.authService.setToken(result);
          this.router.navigateByUrl('trucks-info');
        },
        errorResult => {
          this.spinnerService.hideSpinner();
          this.loginFormGroup.controls.password.reset('');
          this.loginFormGroup.controls.password.setErrors(null);

          if (errorResult.status === 400) {
            this.openDialog({
              title: 'Помилка',
              content: 'Електронна пошта або пароль введені невірно.'
            });
          } else {
            this.openDialog({
              title: 'Помилка',
              content:
                'Внутрішня помилка сервера, будь ласка спробуйте пізніше.'
            });
          }
        }
      );
    }
  }

  private openDialog(dialogData: any): void {
    this.dialog.open(InfoDialogComponent, {
      width: '350px',
      data: dialogData
    });
  }
}
