// Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/material.module';

// Components
import { NodeLoadTabComponent } from './components/node-load-tab/node-load-tab.component';
import { NodeLoadChartComponent } from './components/node-load-chart/node-load-chart.component';

@NgModule({
  declarations: [NodeLoadTabComponent, NodeLoadChartComponent],
  imports: [CommonModule, MaterialModule],
  exports: [NodeLoadTabComponent, NodeLoadChartComponent]
})
export class NodeLoadModule {}
