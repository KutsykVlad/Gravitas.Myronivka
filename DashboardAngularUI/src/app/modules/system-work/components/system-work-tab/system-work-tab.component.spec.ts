import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemWorkTabComponent } from './system-work-tab.component';

describe('SystemWorkTabComponent', () => {
  let component: SystemWorkTabComponent;
  let fixture: ComponentFixture<SystemWorkTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SystemWorkTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemWorkTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
