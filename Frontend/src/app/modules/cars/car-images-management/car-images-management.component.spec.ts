import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarImagesManagementComponent } from './car-images-management.component';

describe('CarImagesManagementComponent', () => {
  let component: CarImagesManagementComponent;
  let fixture: ComponentFixture<CarImagesManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarImagesManagementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarImagesManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
