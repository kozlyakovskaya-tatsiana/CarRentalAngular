import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-message-typing-box',
  templateUrl: './message-typing-box.component.html',
  styleUrls: ['./message-typing-box.component.css']
})
export class MessageTypingBoxComponent {

  constructor() { }

  message: string;
  @Output() sendMessage: EventEmitter<string> = new EventEmitter<string>();

  onPressKey(event: KeyboardEvent): void{
    if (event.key === 'Enter'){
      this.sendMessage.emit(this.message);
      this.message = '';
    }
  }
}
