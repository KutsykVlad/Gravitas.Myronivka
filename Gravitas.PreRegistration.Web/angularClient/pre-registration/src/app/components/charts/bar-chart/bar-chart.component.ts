import { Component, OnInit, Input } from '@angular/core';
import { BarChartInfo } from 'src/app/models/barChartinfo';
import { BarChartColor } from 'src/app/models/barChartColor';

const BorderColor = 'white';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.scss']
})
export class BarChartComponent implements OnInit {
  @Input() public barChartInfo: BarChartInfo;
  @Input() private barColor: string;

  public barChartType = 'bar';
  public barChartOptions = {
    tooltips: {enabled: false},
    hover: {mode: null},
    legend: { display: false},
    scales: {
      yAxes: [
        {
          ticks: {
            display: false
          }
        }
      ],
      xAxes: [
        {
          ticks: {
            display: false
          }
        }
      ]
    }
  };
  public barChartColors: BarChartColor[] = [];

  constructor() {}

  ngOnInit() {
    this.refreshChartColors();
  }

  private refreshChartColors() {
    const barchartColor = new BarChartColor();
    barchartColor.borderColor = BorderColor;
    barchartColor.backgroundColor = [];

// tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < this.barChartInfo.labels.length; ++i) {
      barchartColor.backgroundColor.push(this.barColor);
    }
    for (let i = 0; i < this.barChartInfo.dataCount; ++i) {
      this.barChartColors.push(barchartColor);
    }
  }
}
