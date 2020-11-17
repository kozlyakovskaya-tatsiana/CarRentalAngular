import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentalPointCreateFormComponent } from './rental-point-create-form.component';

describe('RentalPointCreateFormComponent', () => {
  let component: RentalPointCreateFormComponent;
  let fixture: ComponentFixture<RentalPointCreateFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RentalPointCreateFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RentalPointCreateFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
