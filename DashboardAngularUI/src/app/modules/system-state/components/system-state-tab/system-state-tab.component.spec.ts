import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemStateTabComponent } from './system-state-tab.component';

describe('SystemStateTabComponent', () => {
  let component: SystemStateTabComponent;
  let fixture: ComponentFixture<SystemStateTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SystemStateTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemStateTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
