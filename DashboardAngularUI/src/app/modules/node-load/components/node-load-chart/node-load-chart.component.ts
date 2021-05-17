import {
  Component,
  OnInit,
  Input,
  ViewChild,
  OnChanges,
  OnDestroy
} from '@angular/core';
import { Node } from 'src/app/models/node';
import { Chart } from 'chart.js';
import { Subject, interval } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { NodeLoad } from 'src/app/models/node-load';
import { NodesService } from 'src/app/services/nodes.service';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-node-load-chart',
  templateUrl: './node-load-chart.component.html',
  styleUrls: ['./node-load-chart.component.scss']
})
export class NodeLoadChartComponent implements OnInit, OnChanges, OnDestroy {
  @Input() public node: Node = {} as Node;
  @Input() public chartColor = '#24b7e5';

  @ViewChild('nodeLoadChart') private lineChartRef;

  public lineChart: any;
  public nodeLoadData: NodeLoad[] = new Array(600);
  private lineChartData: any[];
  private lineChartColumns: any[];

  private unsubscribe$: Subject<void> = new Subject();

  constructor(private nodesService: NodesService, private alertService: AlertService) {}

  ngOnInit() {}

  ngOnChanges() {
    this.nodeLoadData = new Array(600);
    this.stopDataLoading();
    this.unsubscribe$ = new Subject();
    this.startDataLoading();
  }

  ngOnDestroy() {
    this.stopDataLoading();
  }

  private startDataLoading(): void {
    this.getNodeLoadData();
    const requestInterval = interval(10000);
    requestInterval
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(() => this.getNodeLoadData());
  }

  private getNodeLoadData(): void {
    if (this.node) {
      this.nodesService.getNodeLoad(this.node.Id).subscribe(res => {
        this.nodeLoadData.splice(0, 1);

        const currentDay = new Date();
        let currentTime = currentDay.getHours().toString() + ':';

        if (currentDay.getMinutes() < 10) {
          currentTime += '0' + currentDay.getMinutes().toString() + ':';
        } else {
          currentTime += currentDay.getMinutes().toString() + ':';
        }

        if (currentDay.getSeconds() < 10) {
          currentTime += '0' + currentDay.getSeconds().toString();
        } else {
          currentTime += currentDay.getSeconds().toString();
        }

        this.nodeLoadData.push({
          NodeLoad: res,
          LoadTime: currentTime
        });

        this.createChart();
      },
      error => {
        this.alertService.error('Під час завантаження даних навантаженості вузла сталася помилка.');
      });
    }
  }

  private createChart(): void {
    if (this.lineChart) {
      this.lineChart.destroy();
    }

    this.lineChartColumns = this.nodeLoadData.map(nodeLoad => {
      return nodeLoad.LoadTime;
    });
    this.lineChartData = this.nodeLoadData.map((nodeLoad, index) => {
      return { x: index, y: nodeLoad.NodeLoad };
    });

    this.lineChart = new Chart(this.lineChartRef.nativeElement, {
      type: 'line',
      data: {
        labels: this.lineChartColumns,
        datasets: [
          {
            data: this.lineChartData,
            backgroundColor: this.chartColor
          }
        ]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        legend: {
          display: false
        },
        animation: false,
        scales: {
          xAxes: [
            {
              display: false
            }
          ],
          yAxes: [
            {
              display: false,
              ticks: {
                beginAtZero: true,
                min: 0,
                max: 250000,
                stepSize: 150000
              }
            }
          ]
        },
        elements: {
          point: {
            radius: 0,
            hoverRadius: 2
          },
          line: {
            tension: 0,
            borderWidth: 0
          }
        },
        tooltips: {
          mode: 'nearest',
          intersect: false
        }
      }
    });
  }

  private stopDataLoading(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
