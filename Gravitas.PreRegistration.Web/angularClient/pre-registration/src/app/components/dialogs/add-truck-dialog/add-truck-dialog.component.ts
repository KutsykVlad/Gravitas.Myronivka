import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SpinnerService } from 'src/app/services/spinner.service';
import { InfoDialogComponent } from '../info-dialog/info-dialog.component';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ProductDataService } from 'src/app/services/product-data.service';
import { PreRegistrationService } from 'src/app/services/pre-registration.service';
import { ProductItemDto } from 'src/app/models/productItemDto';

@Component({
  selector: 'app-add-truck-dialog',
  templateUrl: './add-truck-dialog.component.html',
  styleUrls: ['./add-truck-dialog.component.scss']
})
export class AddTruckDialogComponent implements OnInit {
  public productItems: ProductItemDto[];
  public addTruckForm: any;

  public productDataServiceSub: any;

  private PHONE_REGEXP = /^\+?[0-9]{10,14}$/;

  constructor(
    public dialogRef: MatDialogRef<AddTruckDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public preRegistrationService: PreRegistrationService,
    public dialog: MatDialog,
    private spinnerService: SpinnerService,
    private dataService: ProductDataService
  ) {}

  ngOnInit() {
    this.loadProductsData();
    this.addTruckForm = new FormGroup({
      phoneNo: new FormControl('', [
        Validators.required,
        Validators.pattern(this.PHONE_REGEXP)
      ]),
      truckNumber: new FormControl('', [
        Validators.required
      ]),
      notice: new FormControl('')
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  public addTruck() {
    if (this.addTruckForm.valid) {
      this.spinnerService.showSpinner();
      this.preRegistrationService
        .addTruck({
          routeId: this.data.routeId,
          registerDateTime:  this.data.registerDateTime,
          phoneNo: this.addTruckForm.controls.phoneNo.value,
          truckNumber: this.addTruckForm.controls.truckNumber.value,
          notice: this.addTruckForm.controls.notice.value
        })
        .subscribe(
          result => {
            this.spinnerService.hideSpinner();
            this.dataService.refreshData();
            this.openInfoDialog({
              title: 'Реєстрація водія',
              content:
                'Заявка прийнята. Водій отримає повідомлення із датою прибуття протягом кількох хвилин.'
            });
          },
          errorResult => {
            if (errorResult.status === 400) {
              this.openInfoDialog({
                title: 'Помилка',
                content: errorResult.error.message
              });
            } else {
              this.openInfoDialog({
                title: 'Помилка',
                content:
                  'Внутрішня помилка сервера, будь ласка спробуйте пізніше.'
              });
            }
            this.spinnerService.hideSpinner();
          }
        );
      this.addTruckForm.reset();
      this.close();
    }
  }

  private loadProductsData() {
    this.spinnerService.showSpinner();
    this.preRegistrationService.getProducts().subscribe(
      res => {
        this.spinnerService.hideSpinner();
        this.productItems = res;
      },
      errorResult => {
        if (errorResult.status === 400) {
          this.openInfoDialog({
            title: 'Помилка',
            content: errorResult.error.message
          });
        } else {
          this.openInfoDialog({
            title: 'Помилка',
            content: 'Внутрішня помилка сервера,будь ласка спробуйте пізніше'
          });
        }
        this.spinnerService.hideSpinner();
      }
    );
  }

  private openInfoDialog(dialogData: any): void {
    this.dialog.open(InfoDialogComponent, {
      width: '450px',
      data: dialogData
    });
  }
}
