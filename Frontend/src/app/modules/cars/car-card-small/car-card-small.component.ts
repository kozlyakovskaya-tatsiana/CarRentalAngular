import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-car-card-small',
  templateUrl: './car-card-small.component.html',
  styleUrls: ['./car-card-small.component.css']
})
export class CarCardSmallComponent implements OnInit {

  constructor() { }
  @Input() name: string;
  @Input() cost: number;
  @Input() year: number;
  @Input() transmission: string;
  @Input() status: number;
  @Input() btnName: string;
  @Input() imgSrc: string;
  @Input() passengersAmount: string;
  @Input() doorsAmount: number;
  ngOnInit(): void {
  }

}
