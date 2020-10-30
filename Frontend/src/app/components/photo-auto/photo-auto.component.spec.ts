import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotoAutoComponent } from './photo-auto.component';

describe('PhotoAutoComponent', () => {
  let component: PhotoAutoComponent;
  let fixture: ComponentFixture<PhotoAutoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhotoAutoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PhotoAutoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
