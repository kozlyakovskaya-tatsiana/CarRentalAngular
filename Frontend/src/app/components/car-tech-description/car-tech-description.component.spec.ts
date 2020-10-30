import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarTechDescriptionComponent } from './car-tech-description.component';

describe('TechDescriptionComponent', () => {
  let component: CarTechDescriptionComponent;
  let fixture: ComponentFixture<CarTechDescriptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarTechDescriptionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarTechDescriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
