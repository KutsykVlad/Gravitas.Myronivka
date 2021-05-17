import { Component, ViewChild, Input, OnChanges } from '@angular/core';
import { Chart } from 'chart.js';
import { TotalCarsInQueue } from 'src/app/models/total-cars-in-queue';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import ChartDoghnutLabel from 'chartjs-plugin-doughnutlabel';

@Component({
  selector: 'app-total-cars-chart',
  templateUrl: './total-cars-chart.component.html',
  styleUrls: ['./total-cars-chart.component.scss']
})
export class TotalCarsChartComponent implements OnChanges {
  @ViewChild('totalCarsChart') private pieChartRef;
  @Input() totalCarsData: TotalCarsInQueue;
  public pieChart: any;
  private pieChartData: any[];
  private pieChartColumns: any[];

  constructor() {}

  ngOnChanges() {
    if (this.totalCarsData) {
      this.createChart();
    }
  }

  createChart(): void {
    if (this.pieChart) {
      this.pieChart.destroy();
    }

    this.pieChartColumns = ['В черзі', 'На території'];
    this.pieChartData = [
      {
        data: [this.totalCarsData.carsInQueue, this.totalCarsData.carsInside],
        backgroundColor: ['#24b7e5', '#7166ba']
      }
    ];

    this.pieChart = new Chart(this.pieChartRef.nativeElement, {
      type: 'doughnut',
      data: {
        labels: this.pieChartColumns,
        datasets: this.pieChartData
      },
      plugins: [ChartDataLabels, ChartDoghnutLabel],
      options: {
        responsive: true,
        maintainAspectRatio: false,
        legend: {
          display: true,
          onClick: null
        },
        title: {
          display: true,
          text: 'Загальний стан черги',
          position: 'bottom'
        },
        scales: {},
        plugins: {
          datalabels: {
            color: 'white',
            textAlign: 'center',
            font: {
              lineHeight: 1.6,
              size: (1.6 * window.innerHeight) / 100
            },
            formatter: function(value) {
              if (value !== 0) {
                return value;
              } else {
                return '';
              }
            }
          },
          doughnutlabel: {
            labels: [
              {
                text:
                  this.totalCarsData.carsInQueue +
                  this.totalCarsData.carsInside,
                font: {
                  size: (3 * window.innerHeight) / 100,
                  weight: 'bold'
                },
                color: '#0f3d5c'
              }
            ]
          }
        },
        animation: {
          animateRotate: false
        }
      }
    });
  }
}
