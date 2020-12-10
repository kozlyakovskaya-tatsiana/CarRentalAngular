import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-sent-message',
  templateUrl: './sent-message.component.html',
  styleUrls: ['./sent-message.component.css']
})
export class SentMessageComponent {

  constructor() { }

  @Input() message: string;
  @Input() date: string;

}
