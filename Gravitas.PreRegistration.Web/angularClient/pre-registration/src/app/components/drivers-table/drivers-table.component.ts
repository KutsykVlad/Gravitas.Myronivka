import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { PreRegistrationService } from 'src/app/services/pre-registration.service';
import { Truck } from 'src/app/models/truck';
import { DatePipe } from '@angular/common';
import { SpinnerService } from 'src/app/services/spinner.service';
import { MatPaginator } from '@angular/material/paginator';
import { ConfirmDialogComponent } from '../dialogs/confirm-dialog/confirm-dialog.component';
import { InfoDialogComponent } from '../dialogs/info-dialog/info-dialog.component';
import { MatDialog } from '@angular/material';
import { ProductDataService } from 'src/app/services/product-data.service';

@Component({
  selector: 'app-drivers-table',
  templateUrl: './drivers-table.component.html',
  styleUrls: ['./drivers-table.component.scss']
})
export class DriversTableComponent implements OnInit, OnDestroy {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  public skip = 0;
  public paginatorPageSize = 5;
  public paginatorPageSizeOptions: number[] = [5, 10, 20, 50];
  public totalTrucksCount = 0;

  public displayedColumns: string[] = [
    'position',
    'name',
    'date',
    'phone',
    'truckNumber',
    'notice',
    'delete'
  ];
  public dataSource: Truck[];
  public productDataServiceSub: any;

  constructor(
    public preRegistrationService: PreRegistrationService,
    private datePipe: DatePipe,
    public dialog: MatDialog,
    private productDataService: ProductDataService,
    private spinnerService: SpinnerService,
    private dataService: ProductDataService
  ) {}

  ngOnInit() {
    this.loadTruckData();
    this.productDataServiceSub = this.dataService.dataShouldChange.subscribe(
      res => {
        this.loadTruckData();
      }
    );
  }

  ngOnDestroy() {
    if (this.productDataServiceSub) {
      this.productDataServiceSub.unsubscribe();
    }
  }

  public getPagingTruckData(event) {
    this.paginatorPageSize = event.pageSize;
    this.skip = event.pageIndex * event.pageSize;

    this.loadTruckData();
  }

  public confirmTruckDeleting(phonenumber: string) {
    const dialogRef = this.openConfirmDialog({
      title: 'Видалити автомобіль?',
      content: `Телефон ${phonenumber}, цю дію неможливо відмінити!`
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteTruck(phonenumber);
      }
    });
  }

  public isPastDate(date: string) {
    const now = new Date(Date.now());
    const dateForCompare = new Date(date);
    if (dateForCompare > now) {
      return false;
    }
    return true;
  }

  private loadTruckData() {
    this.spinnerService.showSpinner();
    this.preRegistrationService.getTrucks(this.skip, this.paginatorPageSize).subscribe(
      result => {
        this.dataSource = result.result;
        this.totalTrucksCount = result.totalCount;
        this.spinnerService.hideSpinner();
      },
      errorResult => {
        this.spinnerService.hideSpinner();
        if (errorResult.status === 400) {
          this.openInfoDialog({
            title: 'Помилка',
            content: errorResult.error.message
          });
        } else {
          if (errorResult.status !== 401) {
            this.openInfoDialog({
              title: 'Помилка',
              content: 'Внутрішня помилка сервера, будь ласка спробуйте пізніше'
            });
          }
        }
      }
    );
  }

  private openInfoDialog(dialogData: any): void {
    this.dialog.open(InfoDialogComponent, {
      width: '450px',
      data: dialogData
    });
  }

  private openConfirmDialog(dialogData: any) {
    return this.dialog.open(ConfirmDialogComponent, {
      width: '300px',
      data: dialogData
    });
  }

  private deleteTruck(phonenumber: string) {
    this.spinnerService.showSpinner();
    this.preRegistrationService.deleteTruck(phonenumber).subscribe(
      result => {
        this.spinnerService.hideSpinner();
        this.paginator.firstPage();
        this.loadTruckData();
        this.productDataService.refreshData();
      },
      errorResult => {
        this.spinnerService.hideSpinner();
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
      }
    );
  }
}
