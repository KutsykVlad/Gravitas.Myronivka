// Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/material.module';

// Components
import { SystemStateTabComponent } from './components/system-state-tab/system-state-tab.component';

@NgModule({
  declarations: [SystemStateTabComponent],
  imports: [CommonModule, MaterialModule],
  exports: [SystemStateTabComponent]
})
export class SystemStateModule {}
