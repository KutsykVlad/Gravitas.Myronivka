import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TotalCarsChartComponent } from './total-cars-chart.component';

describe('TotalCarsChartComponent', () => {
  let component: TotalCarsChartComponent;
  let fixture: ComponentFixture<TotalCarsChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TotalCarsChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TotalCarsChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
