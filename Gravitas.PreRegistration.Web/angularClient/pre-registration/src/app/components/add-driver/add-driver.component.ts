import {
  Component,
  ChangeDetectionStrategy,
  OnInit,
  OnDestroy
} from '@angular/core';
import {
  isSameDay,
  isSameMonth,
} from 'date-fns';
import { Subject } from 'rxjs';
import {
  CalendarEvent,
  CalendarEventTimesChangedEvent,
  CalendarView
} from 'angular-calendar';
import { DatePipe } from '@angular/common';
import { FormGroup, FormControl } from '@angular/forms';
import { AddTruckDialogComponent } from '../dialogs/add-truck-dialog/add-truck-dialog.component';
import { MatDialog } from '@angular/material';
import { ProductDataService } from 'src/app/services/product-data.service';
import { ProductItemDto } from 'src/app/models/productItemDto';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  },
  green: {
    primary: '#1BC98E'
  }
};

@Component({
  selector: 'app-add-driver',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './add-driver.component.html',
  styleUrls: ['./add-driver.component.scss']
})
export class AddDriverComponent implements OnInit, OnDestroy {
  public view: CalendarView = CalendarView.Month;
  public CalendarView = CalendarView;
  public viewDate: Date = new Date();
  public refresh: Subject<any> = new Subject();
  public events: CalendarEvent[] = [];
  public activeDayIsOpen = false;
  public locale = 'uk';
  public weekStartsOn = 1;

  public productItems: ProductItemDto[];
  public form: any;

  public productDataServiceSub: any;

  constructor(
    private productDataService: ProductDataService,
    public dialog: MatDialog,
    private datePipe: DatePipe,
    private dataService: ProductDataService
  ) {}

  ngOnInit() {
    this.form = new FormGroup({
      routeId: new FormControl(0)
    });
    this.productDataServiceSub = this.productDataService.productsChanged.subscribe(
      res => {
        this.refreshCalendarData();
      }
    );
  }

  ngOnDestroy() {
    if (this.productDataServiceSub) {
      this.productDataServiceSub.unsubscribe();
    }
  }

  public calendarCreation() {
    this.events = [];
    this.productItems.forEach( (value) => {
      if (value.routeId === this.form.value.routeId && value.freeDateTimeList !== undefined) {
        value.freeDateTimeList.forEach((startTime) => {
          const endTime = new Date(startTime);
          endTime.setHours(endTime.getHours() + 1);
          this.addEvent(new Date(startTime), endTime);
        });
      }
    });
  }

  private refreshCalendarData() {
    this.productItems = this.dataService.getProducts();
    if (this.productItems.length > 0 && this.form.value.routeId === 0) {
      this.form.get('routeId').setValue(this.productItems[0].routeId);
    }
    this.calendarCreation();
    this.refresh.next();
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      this.viewDate = date;
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd
  }: CalendarEventTimesChangedEvent): void {
    this.events = this.events.map(iEvent => {
      if (iEvent === event) {
        return {
          ...event,
          start: newStart,
          end: newEnd
        };
      }
      return iEvent;
    });
  }

  eventClicked(event: CalendarEvent): void {
    const routeTitle = this.productItems.filter(e => e.routeId === this.form.value.routeId)[0].title;
    this.dialog.open(AddTruckDialogComponent, {
      data: {routeId: this.form.value.routeId,
      registerDateTime: this.datePipe.transform(event.start, 'yyyy-MM-ddTHH:mm:ss'),
      time: event.title,
      title: routeTitle}
    });
  }

  addEvent(startDate: Date, endDate: Date): void {
    this.events = [
      ...this.events,
      {
        title: this.datePipe.transform(startDate, 'HH:mm') + ' - '
         + this.datePipe.transform(endDate, 'HH:mm'),
        start: startDate,
        color: colors.green
      }
    ];
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
