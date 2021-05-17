import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ProductsService } from 'src/app/modules/system-work/services/products.service';
import { Product } from 'src/app/models/product';
import { Chart } from 'chart.js';
import { GroupByParams } from 'src/app/enums/group-by-params.enum';
import * as moment from 'moment';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-system-work-tab',
  templateUrl: './system-work-tab.component.html',
  styleUrls: ['./system-work-tab.component.scss']
})
export class SystemWorkTabComponent implements OnInit {
  @ViewChild('productChart') private lineChartRef;

  public selectedProducts: Product[] = [];
  public productsList: Product[] = [];
  public productToSearch = new FormControl();
  public dateFrom = new FormControl();
  public dateTo = new FormControl();
  public isDataLoading = false;
  public groupByParam: GroupByParams = GroupByParams.default;
  public groupByParams = GroupByParams;

  public productsData;
  public lineChart: any;
  private locale = 'uk-UA';
  private chartData = [];
  private chartColumns = [];

  constructor(
    private productsService: ProductsService,
    private alertService: AlertService
  ) {}

  ngOnInit() {
    this.updateProductList();
    this.createChart();
  }

  public updateProductList(): void {
    this.isDataLoading = true;
    this.productsService.getProducts(this.productToSearch.value).subscribe(
      res => {
        this.isDataLoading = false;
        this.productsList = res;

        this.updateSelectionInProductList();
      },
      error => {
        this.isDataLoading = false;
        this.alertService.error('Під час завантаження даних сталася помилка.');
      }
    );
  }

  public selectProduct(product: Product): void {
    if (product.IsSelected) {
      this.removeProduct(product);
    } else {
      product.Color = this.getRandomColor();
      this.selectedProducts.push(product);
    }
    this.updateSelectionInProductList();
    this.createChart();
  }

  public optionClicked(event: Event, product: Product): void {
    event.stopPropagation();
    this.selectProduct(product);
  }

  public removeProduct(product: Product): void {
    const productIndex = this.selectedProducts.findIndex(
      selectedProduct => selectedProduct.Id === product.Id
    );
    this.selectedProducts.splice(productIndex, 1);
    this.updateSelectionInProductList();
    this.createChart();
  }

  private updateSelectionInProductList(): void {
    this.productsList.forEach(product => {
      product.IsSelected = false;
      this.selectedProducts.forEach(selectedProduct => {
        if (selectedProduct.Id === product.Id) {
          product.IsSelected = true;
        }
      });
    });
  }

  private getRandomColor(): string {
    const hue = Math.floor(Math.random() * (Math.floor(360) - 0)) + 0;
    const saturation = Math.floor(Math.random() * (Math.floor(100) - 50)) + 50;
    const lightness = Math.floor(Math.random() * (Math.floor(75) - 25)) + 25;

    const color = `hsl(${hue},${saturation}%,${lightness}%)`;

    return color;
  }

  public createChart(): void {
    this.isDataLoading = true;

    this.productsService
      .getProductData(
        this.selectedProducts.map(product => product.Id),
        this.dateFrom.value ? this.dateFrom.value : null,
        this.dateTo.value ? this.dateTo.value : null
      )
      .subscribe(
        res => {
          this.isDataLoading = false;
          this.productsData = res;

          if (this.lineChart) {
            this.lineChart.destroy();
          }

          this.chartData = [];
          this.groupChartData();

          this.lineChart = new Chart(this.lineChartRef.nativeElement, {
            type: 'line',
            data: {
              labels: this.chartColumns,
              datasets: this.chartData
            },
            options: {
              responsive: true,
              maintainAspectRatio: false,
              legend: {
                display: true
              },
              scales: {
                xAxes: [
                  {
                    display: true
                  }
                ],
                yAxes: [
                  {
                    display: true,
                    ticks: {
                      callback: function(value) {
                        return value + ' т';
                      },
                      beginAtZero: true
                    }
                  }
                ]
              },
              tooltips: {
                callbacks: {
                  title: function(tooltipItem, data) {
                    return data.datasets[tooltipItem[0].datasetIndex].data[
                      tooltipItem[0].index
                    ].x;
                  },
                  label: function(tooltipItem, data) {
                    return (
                      data.datasets[tooltipItem.datasetIndex].label +
                      ': ' +
                      tooltipItem.value.toString() +
                      ' т'
                    );
                  }
                }
              }
            }
          });
        },
        error => {
          this.isDataLoading = false;
          this.alertService.error(
            'Під час завантаження даних сталася помилка.'
          );
        }
      );
  }

  private groupChartData(): void {
    if (this.groupByParam === GroupByParams.default) {
      const options = {
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
        hour: 'numeric',
        minute: 'numeric',
        second: 'numeric',
        hour12: false,
        timeZone: 'Europe/Kiev'
      };

      this.chartColumns = this.productsData
        .map(product => {
          const date = new Date(product.Time);
          return date.toLocaleString(this.locale, options);
        })
        .filter((v, i, a) => a.indexOf(v) === i);

      this.selectedProducts.forEach(selectedProduct => {
        const chartItem = {
          label: selectedProduct.Title,
          data: [],
          fill: false,
          lineTension: 0,
          borderColor: selectedProduct.Color
        };

        chartItem.data = this.productsData
          .filter(product => product.ProductId === selectedProduct.Id)
          .map(item => {
            const date = new Date(item.Time);
            return {
              x: date.toLocaleString(this.locale, options),
              y: item.Value / 1000
            };
          });
        this.chartData.push(chartItem);
      });
    } else if (this.groupByParam === GroupByParams.days) {
      const options = {
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
        timeZone: 'Europe/Kiev'
      };

      this.groupDataByDayMonthYear(options);
    } else if (this.groupByParam === GroupByParams.weeks) {
      this.chartColumns = this.productsData
        .map(product => {
          return (
            moment(product.Time).week() +
            ' тиждень/' +
            moment(product.Time).year()
          );
        })
        .filter((v, i, a) => a.indexOf(v) === i);

      this.selectedProducts.forEach(selectedProduct => {
        const chartItem = {
          label: selectedProduct.Title,
          data: [],
          fill: false,
          lineTension: 0,
          borderColor: selectedProduct.Color
        };

        const filteredProducts = this.productsData
          .filter(product => product.ProductId === selectedProduct.Id)
          .map(product => {
            return {
              ProductId: product.ProductId,
              Value: product.Value,
              Time:
                moment(product.Time).week() +
                ' тиждень/' +
                moment(product.Time).year()
            };
          });

        const dateToFilterBy = filteredProducts
          .filter(product => product.ProductId === selectedProduct.Id)
          .map(product => product.Time)
          .filter((v, i, a) => a.indexOf(v) === i);

        chartItem.data = dateToFilterBy.map(date => {
          let productsValue = 0;
          filteredProducts.forEach(product => {
            if (product.Time === date) {
              productsValue += product.Value;
            }
          });
          return {
            x: date,
            y: productsValue / 1000
          };
        });

        this.chartData.push(chartItem);
      });
    } else if (this.groupByParam === GroupByParams.months) {
      const options = {
        year: 'numeric',
        month: 'numeric',
        timeZone: 'Europe/Kiev'
      };

      this.groupDataByDayMonthYear(options);
    } else if (this.groupByParam === GroupByParams.years) {
      const options = {
        year: 'numeric',
        timeZone: 'Europe/Kiev'
      };

      this.groupDataByDayMonthYear(options);
    }
  }

  private groupDataByDayMonthYear(options): void {
    this.chartColumns = this.productsData
      .map(product => {
        const date = new Date(product.Time);
        return date.toLocaleString(this.locale, options);
      })
      .filter((v, i, a) => a.indexOf(v) === i);

    this.selectedProducts.forEach(selectedProduct => {
      const chartItem = {
        label: selectedProduct.Title,
        data: [],
        fill: false,
        lineTension: 0,
        borderColor: selectedProduct.Color
      };

      const filteredProducts = this.productsData.filter(
        product => product.ProductId === selectedProduct.Id
      );
      const dateToFilterBy = this.productsData
        .filter(product => product.ProductId === selectedProduct.Id)
        .map(product => {
          const date = new Date(product.Time);
          return date.toLocaleString(this.locale, options);
        })
        .filter((v, i, a) => a.indexOf(v) === i);

      chartItem.data = dateToFilterBy.map(date => {
        let productsValue = 0;
        filteredProducts.forEach(product => {
          const productDate = new Date(product.Time).toLocaleString(
            this.locale,
            options
          );
          if (productDate === date) {
            productsValue += product.Value;
          }
        });
        return {
          x: date,
          y: productsValue / 1000
        };
      });

      this.chartData.push(chartItem);
    });
  }
}
