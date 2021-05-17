// Modules
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from './material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { QueueStateModule } from './modules/queue-state/queue-state.module';
import { NodeLoadModule } from './modules/node-load/node-load.module';
import { SystemStateModule } from './modules/system-state/system-state.module';
import { SystemWorkModule } from './modules/system-work/system-work.module';

// Components
import { AppComponent } from './components/app/app.component';

import {MAT_DATE_LOCALE} from '@angular/material';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MaterialModule,
    BrowserAnimationsModule,
    QueueStateModule,
    NodeLoadModule,
    SystemStateModule,
    SystemWorkModule
  ],
  providers: [{ provide: MAT_DATE_LOCALE, useValue: 'uk-UA' }],
  bootstrap: [AppComponent]
})
export class AppModule {}
