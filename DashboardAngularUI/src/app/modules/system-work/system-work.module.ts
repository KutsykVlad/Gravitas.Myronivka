// Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/material.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

// Components
import { SystemWorkTabComponent } from './components/system-work-tab/system-work-tab.component';

@NgModule({
  declarations: [SystemWorkTabComponent],
  imports: [CommonModule, MaterialModule, ReactiveFormsModule, FormsModule],
  exports: [SystemWorkTabComponent]
})
export class SystemWorkModule {}
