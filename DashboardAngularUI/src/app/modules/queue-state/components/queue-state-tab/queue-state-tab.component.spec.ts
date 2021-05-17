import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QueueStateTabComponent } from './queue-state-tab.component';

describe('QueueStateTabComponent', () => {
  let component: QueueStateTabComponent;
  let fixture: ComponentFixture<QueueStateTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QueueStateTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QueueStateTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
