import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductDataService } from 'src/app/services/product-data.service';
import { ProductItemDto } from 'src/app/models/productItemDto';

@Component({
  selector: 'app-truck-chart',
  templateUrl: './truck-chart.component.html',
  styleUrls: ['./truck-chart.component.scss']
})
export class TruckChartComponent implements OnInit, OnDestroy {
  public productItems: ProductItemDto[];

  public doughnutChartLabels = [];
  public doughnutChartData = [[]];
  public doughnutChartType = 'doughnut';
  public doughnutColors = [
    {
      backgroundColor: this.productDataService.colours,
      borderColor: 'white'
    }
  ];
  public truckCounter = 0;
  public doughnutOptions = {
    legend: { display: false }
  };

  public productDataServiceSub: any;

  constructor(
    private productDataService: ProductDataService
  ) {}

  ngOnInit() {
    this.productDataServiceSub = this.productDataService.productsChanged
    .subscribe(res => {
      this.refreshChart();
    });
  }

  ngOnDestroy() {
    if (this.productDataServiceSub) {
      this.productDataServiceSub.unsubscribe();
    }
  }

  private refreshChart() {
    this.productItems = this.productDataService.getProducts();

    const newLabels: string[] = [];
    const newData: number[] = [];
    this.truckCounter = 0;

    for (const item of this.productItems) {
      newLabels.push(item.title);
      newData.push(item.trucksInQueue);
      this.truckCounter += item.trucksInQueue;
    }
    this.doughnutChartLabels = newLabels;
    this.doughnutChartData = [newData];
  }
}
