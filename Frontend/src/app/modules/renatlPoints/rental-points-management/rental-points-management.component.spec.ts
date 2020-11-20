import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentalPointsManagementComponent } from './rental-points-management.component';

describe('RentalPointsManagementComponent', () => {
  let component: RentalPointsManagementComponent;
  let fixture: ComponentFixture<RentalPointsManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RentalPointsManagementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RentalPointsManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
