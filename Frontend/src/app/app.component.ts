import {Component, OnInit} from '@angular/core';
import {AuthorizeService} from './shared/services/authorize.service';
import {ChatService} from './shared/services/chat.service';
import swal from 'sweetalert2';
import {JwtHelperService} from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(private authService: AuthorizeService,
              private chatService: ChatService) {
  }

  private subscribeToEvents(): void {
    if (this.authService.isAdmin || this.authService.isManager) {
      this.chatService.hubConnection.on('ReceiveChatRequest', connectId => {
        console.log('request to chat from ' + connectId);
        swal.fire({
          title: 'Chat request',
          text: 'Start chatting now?',
          showConfirmButton: true,
          showCancelButton: true
        }).then(res => {
          if (res.isConfirmed) {
            if (new JwtHelperService().isTokenExpired(this.authService.accessToken)) {
              this.authService.doRefreshToken().subscribe(
                data => {
                  this.chatService.approveChatRequest(connectId).then(
                    () => {
                      console.log('approved request');
                    });
                });
            }
            else {
              this.chatService.approveChatRequest(connectId).then(
                () => {
                  console.log('approved request');
                });
            }
          }
        });
      });
    }
  }

  ngOnInit(): void {
    if (this.authService.isManager || this.authService.isAdmin){
      this.chatService.createConnection();
      this.chatService.startConnection( )
        .then(() => {
            console.log('Connection started');
            this.subscribeToEvents();
            if (new JwtHelperService().isTokenExpired(this.authService.accessToken)){
              this.authService.doRefreshToken().subscribe(
                data => {
                  this.chatService.joinToManagersGroup().then(() => {
                    console.log('current user join to managers group');
                  }).catch(err => console.log(err));
                });
            }
            else {
              this.chatService.joinToManagersGroup().then(() => {
                console.log('current user join to managers group');
              }).catch(err => console.log(err));
            }
        })
        .catch(err => {
          console.log(err);
        });
    }
  }
}
