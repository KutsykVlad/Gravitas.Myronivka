import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NodeLoadTabComponent } from './node-load-tab.component';

describe('NodeLoadTabComponent', () => {
  let component: NodeLoadTabComponent;
  let fixture: ComponentFixture<NodeLoadTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NodeLoadTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NodeLoadTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
