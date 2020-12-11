import {Component, OnInit} from '@angular/core';
import {AuthorizeService} from './shared/services/authorize.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(private authorizeService: AuthorizeService
              /*private chatService: ChatService*/) {
  }

  /*subscribeOnUserStartChat(): void{
      this.chatService.userStartChat.subscribe(group => {
        console.log('user start chat in group' + group);
      });
    }*/

  ngOnInit(): void {
    /*if (this.authorizeService.isAdmin || this.authorizeService.isManager) {
      this.chatService.joinToManagers();
      this.subscribeOnUserStartChat();
    }*/
  }
}
