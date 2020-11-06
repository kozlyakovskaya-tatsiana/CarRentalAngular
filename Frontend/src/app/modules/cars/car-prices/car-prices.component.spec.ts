import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarPricesComponent } from './car-prices.component';

describe('CarPricesComponent', () => {
  let component: CarPricesComponent;
  let fixture: ComponentFixture<CarPricesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarPricesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarPricesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
