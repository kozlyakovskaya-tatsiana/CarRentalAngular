import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MessageTypingBoxComponent } from './message-typing-box.component';

describe('MessageTypingBoxComponent', () => {
  let component: MessageTypingBoxComponent;
  let fixture: ComponentFixture<MessageTypingBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MessageTypingBoxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MessageTypingBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
