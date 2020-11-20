import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentalPointCreateComponent } from './rental-point-create.component';

describe('RentalPointCreateComponent', () => {
  let component: RentalPointCreateComponent;
  let fixture: ComponentFixture<RentalPointCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RentalPointCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RentalPointCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
