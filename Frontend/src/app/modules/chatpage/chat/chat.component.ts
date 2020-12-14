import {AfterViewInit, Component, OnInit} from '@angular/core';
import {ChatService} from '../../../shared/services/chat.service';
import {Message} from '../../../shared/utils/chat/Message';
import {AuthorizeService} from '../../../shared/services/authorize.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit{

  constructor(private chatService: ChatService,
              public authService: AuthorizeService) {
    this.messages = new Array<Message>();
  }
  messages: Array<Message>;
  isChatting = false;
  group: string;

  public onStartChat(): void{
    this.chatService.createConnection();
    this.chatService.startConnection()
      .then(() => {
        console.log('Connection started');
        this.subscribeToEvents();
        this.chatService.sendChatRequest();
      });
  }

  private subscribeToEvents(): void{
    this.chatService.hubConnection.on('StartChat', group => {
        console.log('group' + group);
        this.isChatting = true;
        this.group = group;
        console.log(this.group);
      });
    this.chatService.hubConnection.on('MessageReceived', mes  => {
      mes = mes as Message;
      mes.type = 'received';
      this.messages.push(mes);
      console.log('received a mes');
    });
  }

  public onSendMessage(message: string): void{
    const mes = new Message();
    mes.text = message;
    mes.sendDateTime = new Date();
    mes.type = 'Sent';
    mes.from = this.authService.userEmail;
    mes.group = this.group;
    this.messages.push(mes);
    this.chatService.sendMessage(mes);
  }

  ngOnInit(): void {
    if (this.authService.isManager || this.authService.isAdmin){
      this.isChatting = true;
      console.log('init');
      this.subscribeToEvents();
    }
  }
}
