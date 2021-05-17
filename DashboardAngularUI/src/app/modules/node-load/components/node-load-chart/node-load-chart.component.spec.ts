import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NodeLoadChartComponent } from './node-load-chart.component';

describe('NodeLoadChartComponent', () => {
  let component: NodeLoadChartComponent;
  let fixture: ComponentFixture<NodeLoadChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NodeLoadChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NodeLoadChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
