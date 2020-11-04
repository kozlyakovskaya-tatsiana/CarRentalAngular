import { TestBed } from '@angular/core/testing';

import { CarManagementAccessGuard } from './car-management-access.guard';

describe('ManagerAccessGuard', () => {
  let guard: CarManagementAccessGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(CarManagementAccessGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
