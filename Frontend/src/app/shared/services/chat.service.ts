import {EventEmitter, Injectable, Output} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {HubConnection} from '@microsoft/signalr';
import {environment} from '../../../environments/environment';
import {Message} from '../utils/chat/Message';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor() {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  @Output() messageReceived: EventEmitter<Message> = new EventEmitter<Message>();
  public hubConnection: HubConnection;

  private createConnection(): void{
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.backendDomain + '/chat')
      .build();
  }

  private startConnection(): void{
    this.hubConnection.start()
      .then(
      () => {
        console.log('Connection started');
      }
    )
      .catch(err => {
        console.log(err);
      });
  }

  sendMessage(mesasge: Message): void {
    this.hubConnection.invoke('NewMessage', mesasge);
  }

  private registerOnServerEvents(): void{
    this.hubConnection.on('MessageReceived', (mes: Message) => {
      this.messageReceived.emit(mes);
    });
  }
}
