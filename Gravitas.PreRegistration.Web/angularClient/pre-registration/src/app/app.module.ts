import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ChartsModule } from 'ng2-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AppRoutingModule } from './app-routing.module';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatPaginatorModule, MatPaginatorIntl } from '@angular/material/paginator';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import localeUa from '@angular/common/locales/uk';

import { AppComponent } from './components/app/app.component';
import { TruckChartComponent } from './components/charts/truck-chart/truck-chart.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { LogInComponent } from './components/log-in/log-in.component';
import { TabsComponent } from './components/tabs/tabs.component';
import { BarChartComponent } from './components/charts/bar-chart/bar-chart.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { DriversTableComponent } from './components/drivers-table/drivers-table.component';
import { ProfileComponent } from './components/profile/profile.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { InformationPageComponent } from './components/information-page/information-page.component';
import { DatePipe, registerLocaleData } from '@angular/common';
import { SpinnerWrapperComponent } from './components/spinner-wrapper/spinner-wrapper.component';
import { getUkrainianPaginatorIntl } from './ukrainian-paginator-intl';
import { InfoDialogComponent } from './components/dialogs/info-dialog/info-dialog.component';
import { ConfirmDialogComponent } from './components/dialogs/confirm-dialog/confirm-dialog.component';
import { AddTruckDialogComponent } from './components/dialogs/add-truck-dialog/add-truck-dialog.component';
import { ChangePasswordDialogComponent } from './components/dialogs/change-password-dialog/change-password-dialog.component';
import { AuthService } from './services/auth.service';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AddDriverComponent } from './components/add-driver/add-driver.component';
import { ProductDataService } from './services/product-data.service';
import { EmailForAdminDialogComponent } from './components/dialogs/email-for-admin-dialog/email-for-admin-dialog.component';
import { ContactsComponent } from './components/contacts/contacts.component';

registerLocaleData(localeUa);

@NgModule({
  declarations: [
    AppComponent,
    TruckChartComponent,
    FooterComponent,
    HeaderComponent,
    LogInComponent,
    TabsComponent,
    BarChartComponent,
    RegistrationComponent,
    DriversTableComponent,
    ProfileComponent,
    InfoDialogComponent,
    NotFoundComponent,
    InformationPageComponent,
    SpinnerWrapperComponent,
    ConfirmDialogComponent,
    AddTruckDialogComponent,
    ChangePasswordDialogComponent,
    AddDriverComponent,
    EmailForAdminDialogComponent,
    ContactsComponent
  ],
  entryComponents: [
    InfoDialogComponent,
    ConfirmDialogComponent,
    AddTruckDialogComponent,
    ChangePasswordDialogComponent,
    EmailForAdminDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ChartsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTabsModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatTableModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatIconModule,
    MatSelectModule,
    MatPaginatorModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory
    })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    AuthService,
    ProductDataService,
    DatePipe,
    { provide: MatPaginatorIntl, useValue: getUkrainianPaginatorIntl() }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
