import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { SpinnerService } from 'src/app/services/spinner.service';
import { InfoDialogComponent } from '../dialogs/info-dialog/info-dialog.component';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  public registerFormGroup: any;

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
    this.registerFormGroup = new FormGroup({
      email: new FormControl('', [
        Validators.required,
        Validators.pattern(this.EMAIL_REGEXP)
      ]),
      password: new FormControl('', Validators.required),
      confirmPassword: new FormControl('', Validators.required)
    });
  }

  public cancel() {
    this.router.navigateByUrl('trucks-info');
  }

  public signUp() {
    if (this.registerFormGroup.valid) {
      this.spinnerService.showSpinner();
      this.authService
        .registerUser({
          email: this.registerFormGroup.value.email,
          password: this.registerFormGroup.value.password,
          confirmPassword: this.registerFormGroup.value.confirmPassword
        })
        .subscribe(
          result => {
            this.openDialog({
              title: 'Реєстрація',
              content:
                'Користувач ' +
                this.authService.
                getEmail() +
                ' успішно зареєстрований.'
            });
            this.authService.login({email: this.registerFormGroup.value.email, password: this.registerFormGroup.value.password})
            .subscribe(
              logResult => {
                this.spinnerService.hideSpinner();
                this.authService.setToken(logResult);
                this.router.navigateByUrl('trucks-info');
              },
              errorResult => {
                this.spinnerService.hideSpinner();
                this.openDialog({
                    title: 'Помилка',
                    content:
                      'Внутрішня помилка сервера, будь ласка спробуйте пізніше.'
                  });
              }
            );
          },
          errorResult => {
            this.registerFormGroup.controls.password.reset();
            this.registerFormGroup.controls.confirmPassword.reset();
            this.registerFormGroup.controls.password.setErrors(null);
            this.registerFormGroup.controls.confirmPassword.setErrors(null);
            if (errorResult.status === 400) {
              let errorMessage = '';
              for (const property in errorResult.error.modelState) {
                if (errorResult.error.modelState.hasOwnProperty(property)) {
                  errorMessage += errorResult.error.modelState[property] + ' ';
                }
              }
              if (errorMessage.includes('is already taken')) {
                errorMessage = 'Дана електронна пошта уже зареєстрована.';
              }
              this.openDialog({
                title: 'Помилка реєстрації',
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
    }
  }

  private openDialog(dialogData: any): void {
    this.dialog.open(InfoDialogComponent, {
      width: '350px',
      data: dialogData
    });
  }
}
