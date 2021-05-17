import {
  Component,
  OnInit,
  OnDestroy,
  ViewEncapsulation,
  Input,
  ViewChild,
  ElementRef
} from '@angular/core';
import { Node } from 'src/app/models/node';
import { NodesService } from 'src/app/services/nodes.service';
import { AlertService } from 'src/app/services/alert.service';
import { StatisticsService } from 'src/app/modules/system-state/services/statistics.service';
import { NodeState } from 'src/app/enums/node-state.enum';
import { Subject, interval, forkJoin } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { StatisticData } from '../../../../models/statistic-data';
import { CarsService } from 'src/app/services/cars.service';
import { RealizationState } from 'src/app/enums/realization-state.enum';
import { CarInfo } from 'src/app/models/car-info';

@Component({
  selector: 'app-system-state-tab',
  templateUrl: './system-state-tab.component.html',
  styleUrls: ['./system-state-tab.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SystemStateTabComponent implements OnInit, OnDestroy {
  @Input() isActive: boolean;
  @ViewChild('pageEnd') pageEnd: ElementRef;

  public isDataLoading = true;
  public statisticData: StatisticData;
  public nodes: Node[];
  public selectedNode: Node;
  public nodeState: NodeState;
  public displayedColumns = [
    'truckNumber',
    'trailerNumber',
    'driverName',
    'phoneNumber',
    'documentType'
  ];
  public tableData: CarInfo[];
  public realizationState = RealizationState;
  public isNodeDataLoading = false;

  private unsubscribe$: Subject<void> = new Subject();

  constructor(
    private nodesService: NodesService,
    private alertService: AlertService,
    private statisticsService: StatisticsService,
    private carsService: CarsService
  ) {}

  ngOnInit() {
    forkJoin([
      this.statisticsService.getStatisticData(),
      this.nodesService.getAllNodes()
    ])
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        res => {
          this.statisticData = res[0];
          this.nodes = res[1];

          this.isDataLoading = false;

          const requestInterval = interval(10000);
          requestInterval.pipe(takeUntil(this.unsubscribe$)).subscribe(() => {
            if (this.isActive) {
              this.getNodesState();
            }
          });

          const requestStatisticInterval = interval(30000);
          requestStatisticInterval
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(() => {
              if (this.isActive) {
                this.getStatisticData();
              }
            });
        },
        error => {
          this.isDataLoading = false;
          this.alertService.error(
            'Під час завантаження даних сталася помилка. Перезавантажте сторінку.'
          );
        }
      );
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  private getStatisticData(): void {
    this.statisticsService.getStatisticData().subscribe(
      res => {
        this.statisticData = res;
      },
      error => {
        this.alertService.error(
          'Під час завантаження статистичних даних сталася помилка.'
        );
      }
    );
  }

  private getNodesState(): void {
    this.nodesService.getAllNodes().subscribe(
      res => {
        this.nodes = res;
      },
      error => {
        this.alertService.error('Під час завантаження даних вузлів сталася помилка.');
      }
    );
  }

  public getNodeState(nodeId: number): string {
    if (this.nodes !== undefined) {
      if (this.nodes[nodeId].State === NodeState.active) {
        return 'active';
      } else if (this.nodes[nodeId].State === NodeState.inWork) {
        return 'in-work';
      } else {
        return 'inactive';
      }
    } else {
      return '';
    }
  }

  public showSelectedNodeData(node: Node): void {
    this.selectedNode = node;
    this.isNodeDataLoading = true;
    this.carsService.getAllCarsInside().subscribe(res => {
      this.tableData = res.filter(
        car =>
          car.FutureNodeIds.filter(nodeId => nodeId === this.selectedNode.Id)
            .length !== 0
      );
      this.isNodeDataLoading = false;
    },
    error => {
      this.alertService.error('Під час завантаження даних вузла сталася помилка.');
      this.isNodeDataLoading = false;
    });

    setTimeout(() => {
      this.pageEnd.nativeElement.scrollIntoView({
        block: 'end',
        behavior: 'smooth'
      });
    }, 100);
  }
}
