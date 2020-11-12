import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-photo-auto',
  templateUrl: './photo-auto.component.html',
  styleUrls: ['./photo-auto.component.css']
})
export class PhotoAutoComponent implements OnInit{

  @Input() imageDataUrls: string[];
  constructor() {}

  ngOnInit(): void {
    console.log(this.imageDataUrls);
  }

}
