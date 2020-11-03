import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-car-photo',
  templateUrl: './car-photo.component.html',
  styleUrls: ['./car-photo.component.css']
})
export class CarPhotoComponent implements OnInit {

  constructor() { }

  @Input() imgSrc: string | ArrayBuffer;
  @Input() imgIndex: number;
  @Output() crossClicked: EventEmitter<number> = new EventEmitter<number>();

  clickCross(index: number): void{
    this.crossClicked.emit(index);
  }

  ngOnInit(): void {
  }

}
