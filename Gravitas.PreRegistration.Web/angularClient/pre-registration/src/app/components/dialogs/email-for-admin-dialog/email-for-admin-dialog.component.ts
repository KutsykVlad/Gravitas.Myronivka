import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { AddTruckDialogComponent } from '../add-truck-dialog/add-truck-dialog.component';
import { InfoDialogComponent } from '../info-dialog/info-dialog.component';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PreRegistrationService } from 'src/app/services/pre-registration.service';
import { SpinnerService } from 'src/app/services/spinner.service';
import { ProductDataService } from 'src/app/services/product-data.service';

@Component({
  selector: 'app-email-for-admin-dialog',
  templateUrl: './email-for-admin-dialog.component.html',
  styleUrls: ['./email-for-admin-dialog.component.scss']
})
export class EmailForAdminDialogComponent implements OnInit {
  public emailForm: any;
  public attachment: any;

  private PHONE_REGEXP = /^\+?[0-9]{10,14}$/;
  constructor(
    public dialogRef: MatDialogRef<AddTruckDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialog: MatDialog,
    public preRegistrationService: PreRegistrationService,
    private spinnerService: SpinnerService,
    private dataService: ProductDataService
  ) {}

  ngOnInit() {
    this.emailForm = new FormGroup({
      companyName: new FormControl('', [Validators.required]),
      enterpriseCode: new FormControl('', [Validators.required]),
      phoneNo: new FormControl('', [
        Validators.required,
        Validators.pattern(this.PHONE_REGEXP)
      ]),
      numberOfRegistrations: new FormControl('', [Validators.required])
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  public sendEmail() {
    if (this.emailForm.valid && this.attachment !== undefined) {
      this.spinnerService.showSpinner();
      this.preRegistrationService
        .sendEmailForAdmin({
          companyName: this.emailForm.value.companyName,
          enterpriseCode: this.emailForm.value.enterpriseCode,
          phoneNo: this.emailForm.value.phoneNo,
          numberOfRegistrations: this.emailForm.value.numberOfRegistrations,
          attachment: this.attachment
        })
        .subscribe(
          () => {
            this.dataService.refreshData();
            this.spinnerService.hideSpinner();
          },
          () => {
            this.spinnerService.hideSpinner();
            this.openInfoDialog({
              title: 'Помилка',
              content:
                'Внутрішня помилка сервера, будь ласка спробуйте пізніше.'
            });
          }
        );
      this.close();
    }
  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      const myReader: FileReader = new FileReader();
      myReader.onloadend = e => {
        this.attachment = myReader.result;
      };
      myReader.readAsDataURL(file);
    }
  }

  private openInfoDialog(dialogData: any): void {
    this.dialog.open(InfoDialogComponent, {
      width: '450px',
      data: dialogData
    });
  }
}
