// Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/material.module';
import { ReactiveFormsModule } from '@angular/forms';

// Components
import { QueueStateTabComponent } from './components/queue-state-tab/queue-state-tab.component';
import { TotalCarsChartComponent } from './components/total-cars-chart/total-cars-chart.component';
import { CarsRealizationChartComponent } from './components/cars-realization-chart/cars-realization-chart.component';
import { CarsNodesChartComponent } from './components/cars-nodes-chart/cars-nodes-chart.component';

@NgModule({
  declarations: [
    QueueStateTabComponent,
    TotalCarsChartComponent,
    CarsRealizationChartComponent,
    CarsNodesChartComponent
  ],
  imports: [CommonModule, ReactiveFormsModule, MaterialModule],
  exports: [
    QueueStateTabComponent,
    TotalCarsChartComponent,
    CarsRealizationChartComponent,
    CarsNodesChartComponent
  ]
})
export class QueueStateModule {}
