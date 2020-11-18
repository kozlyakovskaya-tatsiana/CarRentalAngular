import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapSearchboxComponent } from './map-searchbox.component';

describe('MapSearchboxComponent', () => {
  let component: MapSearchboxComponent;
  let fixture: ComponentFixture<MapSearchboxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MapSearchboxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MapSearchboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
