import {Component, OnInit} from '@angular/core';
import {AuthorizeService} from './services/authorize.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{
  constructor(private authorizeService: AuthorizeService) {
  }
}
