import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-head-image',
  templateUrl: './head-image.component.html',
  styleUrls: ['./head-image.component.css']
})
export class HeadImageComponent implements OnInit {

  constructor() { }

  @Input() mainTitle: string;
  @Input() additionalTitle: string;

  ngOnInit(): void {
  }

}
