import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  ViewEncapsulation,
  Input
} from '@angular/core';
import { Node } from 'src/app/models/node';
import { Chart } from 'chart.js';
import { DomSanitizer } from '@angular/platform-browser';
import { NodesService } from 'src/app/services/nodes.service';
import { interval, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import ChartDoghnutLabel from 'chartjs-plugin-doughnutlabel';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-cars-nodes-chart',
  templateUrl: './cars-nodes-chart.component.html',
  styleUrls: ['./cars-nodes-chart.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class CarsNodesChartComponent implements OnInit, OnDestroy {
  @Input() isActive: boolean;
  @ViewChild('carsNodesChart') private pieChartRef;

  public nodes: Node[];
  public selectedNodes: Node[] = [];
  public pieChart: any;
  public legendData: any;
  public isDataLoading = false;

  private outsideTrucksToNodesData;
  private pieChartData: any[];
  private pieChartColumns: any[];

  private unsubscribe$: Subject<void> = new Subject();
  private stopDataLoading$: Subject<void> = new Subject();

  constructor(
    private nodesService: NodesService,
    private sanitizer: DomSanitizer,
    private alertService: AlertService
  ) {}

  ngOnInit() {
    this.nodesService.getAllNodes().subscribe(res => {
      this.nodes = res;

      this.nodes.forEach(node => {
        node.Color = this.getRandomColor();
      });

      if (this.nodes.length > 2) {
        this.selectedNodes = this.nodes.slice(0, 2);
      } else {
        this.selectedNodes = this.nodes;
      }

      this.getOutsideTrucksToNodes();
      const requestInterval = interval(15000);
      requestInterval.pipe(takeUntil(this.unsubscribe$)).subscribe(() => {
        if (this.isActive) {
          this.getOutsideTrucksToNodes();
        }
      });
    });
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public getOutsideTrucksToNodes(isLoadingSpinner?: boolean): void {
    this.stopDataLoading$.next();
    this.stopDataLoading$.complete();
    this.stopDataLoading$ = new Subject();

    if (isLoadingSpinner) {
      this.isDataLoading = true;
    }

    this.nodesService
      .getOutsideTrucksToNodes(this.selectedNodes.map(node => node.Id))
      .pipe(takeUntil(this.stopDataLoading$))
      .subscribe(
        res => {
          this.isDataLoading = false;
          this.outsideTrucksToNodesData = res;
          this.createChart();
        },
        error => {
          this.isDataLoading = false;
          this.alertService.error(
            'Під час завантаження даних вузла сталася помилка'
          );
        }
      );
  }

  private getRandomColor(): string {
    const hue = Math.floor(Math.random() * (Math.floor(300) - 180)) + 180;
    const saturation = Math.floor(Math.random() * (Math.floor(100) - 50)) + 50;
    const lightness = Math.floor(Math.random() * (Math.floor(75) - 25)) + 25;

    const color = `hsl(${hue},${saturation}%,${lightness}%)`;

    return color;
  }

  private createChart(): void {
    if (this.pieChart) {
      this.pieChart.destroy();
    }

    let carsCount = 0;
    this.pieChartColumns = this.selectedNodes.map(node => node.Name);
    this.pieChartData = [
      {
        data: Object.keys(this.outsideTrucksToNodesData).map(key => {
          carsCount += this.outsideTrucksToNodesData[key];
          return this.outsideTrucksToNodesData[key];
        }),
        backgroundColor: this.selectedNodes.map(node => node.Color)
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
          display: false
        },
        title: {
          display: true,
          text: 'Навантаженість вузлів',
          position: 'bottom'
        },
        scales: {},
        plugins: {
          datalabels: {
            color: 'white',
            textAlign: 'center',
            font: {
              lineHeight: 1.6,
              size: (2 * window.innerHeight) / 100
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
                text: carsCount,
                font: {
                  size: (4 * window.innerHeight) / 100,
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
    this.legendData = this.sanitizer.bypassSecurityTrustHtml(
      this.pieChart.generateLegend()
    );
  }
}
