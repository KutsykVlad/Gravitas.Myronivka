import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { SpinnerService } from 'src/app/services/spinner.service';
import { ConfirmDialogComponent } from '../dialogs/confirm-dialog/confirm-dialog.component';
import { InfoDialogComponent } from '../dialogs/info-dialog/info-dialog.component';
import { ChangePasswordDialogComponent } from '../dialogs/change-password-dialog/change-password-dialog.component';
import { EmailForAdminDialogComponent } from '../dialogs/email-for-admin-dialog/email-for-admin-dialog.component';
import { PreRegistrationService } from 'src/app/services/pre-registration.service';
import { ProductDataService } from 'src/app/services/product-data.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit, OnDestroy {
  public companyHasAllDetails: boolean;
  public productDataServiceSub: any;

  constructor(
    private router: Router,
    public authService: AuthService,
    public preRegistrationService: PreRegistrationService,
    public dialog: MatDialog,
    private spinnerService: SpinnerService,
    private dataService: ProductDataService
  ) {}

  ngOnInit() {
    this.loadData();
    this.productDataServiceSub = this.dataService.dataShouldChange.subscribe(
      res => {
        this.loadData();
      }
    );
  }

  ngOnDestroy() {
    if (this.productDataServiceSub) {
      this.productDataServiceSub.unsubscribe();
    }
  }

  public confirmUserDeleting() {
    const dialogRef = this.openConfirmDialog({
      title: 'Видалити обліковий запис?',
      content: 'Цю дію неможливо відмінити!'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteUser();
      }
    });
  }

  public deleteUser() {
    this.authService.deleteUser().subscribe(
      result => {
        this.spinnerService.hideSpinner();
        this.logout();
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
            content: 'Внутрішня помилка сервера, будь ласка спробуйте пізніше.'
          });
        }
        this.spinnerService.hideSpinner();
      }
    );
  }

  public logout() {
    this.authService.logout();
    this.router.navigateByUrl('trucks-info');
  }

  public showChangePassword() {
    this.dialog.open(ChangePasswordDialogComponent, {});
  }

  public showSendEmailForAdmin() {
    this.dialog.open(EmailForAdminDialogComponent, {});
  }

  private openConfirmDialog(dialogData: any) {
    return this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: dialogData
    });
  }

  private loadData() {
    this.spinnerService.showSpinner();
    this.preRegistrationService.getCompanyDetails().subscribe(
      result => {
        this.spinnerService.hideSpinner();
        this.preRegistrationService.setCompanyDetails(result);
        this.checkIfCompanyHasAllDetails();
      },
      errorResult => {
        this.spinnerService.hideSpinner();
        this.openDialog({
          title: 'Помилка',
          content: 'Не вдалось завантажити дані'
        });
      }
    );
  }

  private openDialog(dialogData: any): void {
    this.dialog.open(InfoDialogComponent, {
      width: '250px',
      data: dialogData
    });
  }

  private checkIfCompanyHasAllDetails() {
    const company = this.preRegistrationService.companyInfo;
    if (
      company !== undefined &&
      company.name !== null &&
      company.name !== undefined &&
      company.name !== ''
    ) {
      this.companyHasAllDetails = true;
    } else {
      this.companyHasAllDetails = false;
    }
  }
}
