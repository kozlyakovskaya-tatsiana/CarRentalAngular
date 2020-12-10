import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-received-message',
  templateUrl: './received-message.component.html',
  styleUrls: ['./received-message.component.css']
})
export class ReceivedMessageComponent{

  constructor() { }

  @Input() message: string;
  @Input() date: string;

}
