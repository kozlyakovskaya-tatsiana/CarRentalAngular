import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateRentalPointByLocationComponent } from './create-rental-point-by-location.component';

describe('CreateRentalPointByLocationComponent', () => {
  let component: CreateRentalPointByLocationComponent;
  let fixture: ComponentFixture<CreateRentalPointByLocationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateRentalPointByLocationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateRentalPointByLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
