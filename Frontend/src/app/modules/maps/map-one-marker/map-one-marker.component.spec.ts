import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapOneMarkerComponent } from './map-one-marker.component';

describe('MapOneMarkerComponent', () => {
  let component: MapOneMarkerComponent;
  let fixture: ComponentFixture<MapOneMarkerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MapOneMarkerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MapOneMarkerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
