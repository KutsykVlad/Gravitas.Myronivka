import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductItem } from 'src/app/models/productItem';
import { DatePipe } from '@angular/common';
import { ProductDataService } from 'src/app/services/product-data.service';
import { SpinnerService } from 'src/app/services/spinner.service';
import { PreRegistrationService } from 'src/app/services/pre-registration.service';
import { ProductItemDto } from 'src/app/models/productItemDto';
import { BarChartInfo } from 'src/app/models/barChartinfo';

@Component({
  selector: 'app-information-page',
  templateUrl: './information-page.component.html',
  styleUrls: ['./information-page.component.scss']
})
export class InformationPageComponent implements OnInit, OnDestroy {
  public displayedColumns: string[] = [
    'position',
    'title',
    'entryDateTime',
    'trucksInQueue'
  ];
  public dataSource: ProductItemDto[];
  public resultData: ProductItem[];
  private productDataServiceSub: any;

  public barChartInfo: BarChartInfo[];

  constructor(
    private preRegistrationService: PreRegistrationService,
    private spinnerService: SpinnerService,
    public productDataService: ProductDataService,
    private datePipe: DatePipe,
  ) {}

  ngOnInit() {
    this.loadData();
    this.refreshTable();
    this.productDataServiceSub = this.productDataService.productsChanged
    .subscribe(res => {
      this.refreshTable();
    });
    this.productDataServiceSub = this.productDataService.dataShouldChange
    .subscribe(res => {
      this.loadData();
    });
  }

  ngOnDestroy() {
    if (this.productDataServiceSub) {
      this.productDataServiceSub.unsubscribe();
    }
  }

  private loadData() {
    this.spinnerService.showSpinner();
    this.preRegistrationService.getProducts().subscribe(
      res => {
        this.productDataService.setProducts(res);
        this.loadBarChartData();
        this.spinnerService.hideSpinner();
      },
      error => {
        this.spinnerService.hideSpinner();
      }
    );
  }

  private loadBarChartData() {
    this.preRegistrationService.getBarChartInfo().subscribe(
      res => {
        this.barChartInfo = res;
      });
  }

  private refreshTable() {
    this.resultData = [];
    this.dataSource = this.productDataService.getProducts();
    let entryDate = '';
    // tslint:disable-next-line:forin
    for (const i in this.dataSource) {
      if (this.dataSource[i].freeDateTimeList !== undefined) {
        entryDate = this.dataSource[i].freeDateTimeList[0].toString();

        entryDate = this.datePipe.transform(entryDate, 'dd.MM.yyyy HH:mm');
      }

      this.resultData.push({
        entryDateTime: entryDate,
        routeId: this.dataSource[i].routeId,
        title: this.dataSource[i].title,
        trucksInQueue: this.dataSource[i].trucksInQueue
      });
    }
  }
}
