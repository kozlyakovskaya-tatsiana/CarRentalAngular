import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarCardSmallComponent } from './car-card-small.component';

describe('CarCardSmallComponent', () => {
  let component: CarCardSmallComponent;
  let fixture: ComponentFixture<CarCardSmallComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarCardSmallComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarCardSmallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
