import { Component, ViewChild, Input, OnChanges } from '@angular/core';
import { TotalCarsRealizationState } from 'src/app/models/total-cars-realization-state';
import { Chart } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import ChartDoghnutLabel from 'chartjs-plugin-doughnutlabel';

@Component({
  selector: 'app-cars-realization-chart',
  templateUrl: './cars-realization-chart.component.html',
  styleUrls: ['./cars-realization-chart.component.scss']
})
export class CarsRealizationChartComponent implements OnChanges {
  @ViewChild('carsRealizationChart') private pieChartRef;
  @Input() totalCarsRealizationData: TotalCarsRealizationState;
  public pieChart: any;
  private pieChartData: any[];
  private pieChartColumns: any[];

  constructor() {}

  ngOnChanges() {
    if (this.totalCarsRealizationData) {
      this.createChart();
    }
  }

  createChart(): void {
    this.pieChartColumns = ['Прихід', 'Розхід'];
    this.pieChartData = [
      {
        data: [
          this.totalCarsRealizationData.carsApproach,
          this.totalCarsRealizationData.carsСonsumption
        ],
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
          text: 'Співвідношення Прихід/Розхід',
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
                  this.totalCarsRealizationData.carsApproach +
                  this.totalCarsRealizationData.carsСonsumption,
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
