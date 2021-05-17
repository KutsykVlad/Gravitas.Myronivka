import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CarsRealizationChartComponent } from './cars-realization-chart.component';

describe('CarsRealizationChartComponent', () => {
  let component: CarsRealizationChartComponent;
  let fixture: ComponentFixture<CarsRealizationChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CarsRealizationChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CarsRealizationChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
