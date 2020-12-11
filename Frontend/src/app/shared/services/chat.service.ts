import {EventEmitter, Injectable, Output} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {HubConnection} from '@microsoft/signalr';
import {Message} from '../utils/chat/Message';
import {AuthorizeService} from './authorize.service';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private authService: AuthorizeService) {
  }

  public hubConnection: HubConnection;

  public createConnection(): void{
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl( environment.backendDomain + '/chat', { accessTokenFactory: () => this.authService.accessToken})
      .build();
  }

  public startConnection(): Promise<any>{
    return this.hubConnection.start();
  }

  public sendChatRequest(): Promise<any>{
    return this.hubConnection.invoke('SendChatRequest');
  }

  sendMessage(message: Message): Promise<any> {
    return this.hubConnection.invoke('NewMessage', message);
  }

  approveChatRequest(connectionId: string): Promise<any>{
    return this.hubConnection.invoke('ApproveChatRequest', connectionId);
  }

  joinToManagersGroup(): Promise<any> {
    return this.hubConnection.invoke('JoinToManagersGroup');
  }
}
