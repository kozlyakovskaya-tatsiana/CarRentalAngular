import {AfterViewInit, Component, OnInit} from '@angular/core';
import {ChatService} from '../../../shared/services/chat.service';
import {Message} from '../../../shared/utils/chat/Message';
import {AuthorizeService} from '../../../shared/services/authorize.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, AfterViewInit {

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
      }
    )
      .catch(err => {
        this.authService.doRefreshToken().subscribe(
          data => {
            this.chatService.hubConnection.start()
              .then(() => {
                console.log('Connection started');
                this.subscribeToEvents();
                this.chatService.sendChatRequest();
              })
              .catch(() => {
                console.log('Another error while startConnection');
              });
          });
        });
  }

  public approveChatRequest(): void{

  }

  private setToBottomScroll(): void{
    const box = document.getElementById('message-list');
    box.scrollTop = box.scrollHeight;
  }

  private subscribeToEvents(): void{
    if (this.authService.isAdmin || this.authService.isManager) {
      this.chatService.hubConnection.on('ReceiveChatRequest', connectId => {
        console.log('request' + connectId);
        this.chatService.approveChatRequest(connectId).then(
          () => {
            console.log('createg group');
          });
      });
    }
    this.chatService.hubConnection.on('StartChat', group => {
        console.log('group' + group);
        this.isChatting = true;
        this.group = group;
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
    /*mes.to = 'admin@gmail.com';*/
    mes.group = /*(new Guid()).toString()*/'myGroup';
    this.messages.push(mes);
    this.chatService.sendMessage(mes);
    console.log(mes);
  }
  ngOnInit(): void {
    if (this.authService.isManager || this.authService.isAdmin){
      this.chatService.createConnection();
      this.chatService.startConnection()
        .then(() => {
            console.log('Connection started');
            this.subscribeToEvents();
            this.chatService.joinToManagersGroup();
          }
        )
        .catch(err => {
          this.authService.doRefreshToken().subscribe(
            data => {
              this.chatService.hubConnection.start()
                .then(() => {
                  console.log('Connection started');
                  this.chatService.joinToManagersGroup().then(
                    res => {
                      console.log('success adding to managers group');
                    },
                    e => {
                      console.log(e);
                    }
                  );
                  this.subscribeToEvents();
                })
                .catch(() => {
                  console.log('Another error while startConnection');
                });
            });
        });
    }

  }

  ngAfterViewInit(): void {
    this.setToBottomScroll();
  }

}
