import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EdituserByAdminComponent } from './edituser-by-admin.component';

describe('EdituserByAdminComponent', () => {
  let component: EdituserByAdminComponent;
  let fixture: ComponentFixture<EdituserByAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EdituserByAdminComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EdituserByAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
