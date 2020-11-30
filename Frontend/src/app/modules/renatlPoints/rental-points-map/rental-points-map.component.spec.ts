import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentalPointsMapComponent } from './rental-points-map.component';

describe('RentalPointsMapComponent', () => {
  let component: RentalPointsMapComponent;
  let fixture: ComponentFixture<RentalPointsMapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RentalPointsMapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RentalPointsMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
