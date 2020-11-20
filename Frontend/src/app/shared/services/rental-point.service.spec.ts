import { TestBed } from '@angular/core/testing';

import { RentalPointService } from './rental-point.service';

describe('RentalPointService', () => {
  let service: RentalPointService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RentalPointService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
