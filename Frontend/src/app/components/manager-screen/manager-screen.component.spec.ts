import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MangerScreenComponent } from './manger-screen.component';

describe('MangerScreenComponent', () => {
  let component: MangerScreenComponent;
  let fixture: ComponentFixture<MangerScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MangerScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MangerScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
