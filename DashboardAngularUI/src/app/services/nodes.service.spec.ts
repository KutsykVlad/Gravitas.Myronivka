import { TestBed } from '@angular/core/testing';

import { NodesService } from './nodes.service';

describe('NodesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NodesService = TestBed.get(NodesService);
    expect(service).toBeTruthy();
  });
});
