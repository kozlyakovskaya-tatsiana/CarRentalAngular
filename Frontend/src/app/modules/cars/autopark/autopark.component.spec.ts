import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutoparkComponent } from './autopark.component';

describe('AutoparkComponent', () => {
  let component: AutoparkComponent;
  let fixture: ComponentFixture<AutoparkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AutoparkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AutoparkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
