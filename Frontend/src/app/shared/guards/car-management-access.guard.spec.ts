import { TestBed } from '@angular/core/testing';

import { AdminManagerAccessGuard } from './admin-manager-access-guard.service';

describe('ManagerAccessGuard', () => {
  let guard: AdminManagerAccessGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AdminManagerAccessGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
