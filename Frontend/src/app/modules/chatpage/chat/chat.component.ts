import {AfterViewInit, Component, OnInit} from '@angular/core';
import {ChatService} from '../../../shared/services/chat.service';
import {Message} from '../../../shared/utils/chat/Message';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, AfterViewInit {

  constructor(private chatService: ChatService) {
    this.subscribeToEvents();
    this.messages = new Array<Message>();
  }

  messages: Array<Message> = new Array<Message>();

  private setToBottomScroll(): void{
    const box = document.getElementById('message-list');
    box.scrollTop = box.scrollHeight;
  }

  private subscribeToEvents(): void{
    this.chatService.messageReceived.subscribe((message) => {
      message.type = 'received';
      this.messages.push(message);
      console.log(message);
    });
  }

  public onSendMessage(message: string): void{
    const mes = new Message();
    mes.text = message;
    mes.sendDateTime = new Date();
    mes.type = 'Sent';
    this.messages.push(mes);
    this.chatService.sendMessage(mes);
    console.log(mes);
  }
  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.setToBottomScroll();
  }

}
