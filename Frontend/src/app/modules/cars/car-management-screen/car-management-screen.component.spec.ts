import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarManagementScreenComponent } from './car-management-screen.component';

describe('CarManagementScreenComponent', () => {
  let component: CarManagementScreenComponent;
  let fixture: ComponentFixture<CarManagementScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarManagementScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarManagementScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
