import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CarsNodesChartComponent } from './cars-nodes-chart.component';

describe('CarsNodesChartComponent', () => {
  let component: CarsNodesChartComponent;
  let fixture: ComponentFixture<CarsNodesChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CarsNodesChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CarsNodesChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
