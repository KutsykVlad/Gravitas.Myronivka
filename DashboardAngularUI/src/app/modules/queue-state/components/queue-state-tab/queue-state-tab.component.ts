import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { RealizationState } from 'src/app/enums/realization-state.enum';
import { CarInfo } from 'src/app/models/car-info';
import { TotalCarsInQueue } from 'src/app/models/total-cars-in-queue';
import { TotalCarsRealizationState } from 'src/app/models/total-cars-realization-state';
import { Subject, interval, forkJoin } from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { takeUntil } from 'rxjs/operators';
import { CarsService } from 'src/app/services/cars.service';

@Component({
  selector: 'app-queue-state-tab',
  templateUrl: './queue-state-tab.component.html',
  styleUrls: ['./queue-state-tab.component.scss']
})
export class QueueStateTabComponent implements OnInit, OnDestroy {
  @Input() isActive: boolean;

  public allCarsInside: CarInfo[];
  public allCarsInQueue: CarInfo[];
  public totalCarsData: TotalCarsInQueue;
  public totalCarsRealizationData: TotalCarsRealizationState;
  public isDataLoading = true;

  private unsubscribe$: Subject<void> = new Subject();

  constructor(
    private carsService: CarsService,
    private alertService: AlertService
  ) {}

  ngOnInit() {
    this.getData();

    const requestInterval = interval(15000);
    requestInterval.pipe(takeUntil(this.unsubscribe$)).subscribe(() => {
      if (this.isActive) {
        this.getData();
      }
    });
  }

  ngOnDestroy() {
    this.stopDataLoading();
  }

  private getData(): void {
    forkJoin([
      this.carsService.getAllCarsInQueue(),
      this.carsService.getAllCarsInside()
    ])
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        res => {
          this.isDataLoading = false;

          this.allCarsInQueue = res[0];
          this.allCarsInside = res[1];

          this.totalCarsData = {
            carsInQueue: this.allCarsInQueue.length,
            carsInside: this.allCarsInside.length
          };

          this.totalCarsRealizationData = {
            carsApproach: this.allCarsInQueue
              .concat(this.allCarsInside)
              .filter(car => car.DocumentTypeId === RealizationState.approach)
              .length,
            carsСonsumption: this.allCarsInQueue
              .concat(this.allCarsInside)
              .filter(
                car => car.DocumentTypeId === RealizationState.consumption
              ).length
          };
        },
        error => {
          this.isDataLoading = false;
          this.stopDataLoading();
          this.alertService.error(
            'Під час завантаження даних стану черги сталася помилка.'
          );
        }
      );
  }

  private stopDataLoading(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
