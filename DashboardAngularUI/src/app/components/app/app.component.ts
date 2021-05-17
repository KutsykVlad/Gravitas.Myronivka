import { Component, OnInit, HostListener } from '@angular/core';
import { Chart } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import ChartDoghnutLabel from 'chartjs-plugin-doughnutlabel';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public tabsStatus: boolean[] = [true, false, false, false];

  @HostListener('window:resize', ['$event'])
  onResize() {
    Chart.defaults.global.defaultFontSize = (1.2 * window.innerHeight) / 100;
  }

  ngOnInit() {
    this.onResize();

    Chart.plugins.unregister(ChartDataLabels);
    Chart.plugins.unregister(ChartDoghnutLabel);
  }

  public changeSelectedPage($event) {
    this.tabsStatus.fill(false);
    this.tabsStatus[$event.index] = true;
  }
}
