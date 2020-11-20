import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentalPointTableComponent } from './rental-point-table.component';

describe('RentalPointTableComponent', () => {
  let component: RentalPointTableComponent;
  let fixture: ComponentFixture<RentalPointTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RentalPointTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RentalPointTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
