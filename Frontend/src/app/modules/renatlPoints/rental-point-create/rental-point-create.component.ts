import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-rental-point-create',
  templateUrl: './rental-point-create.component.html',
  styleUrls: ['./rental-point-create.component.css']
})
export class RentalPointCreateComponent implements OnInit {

  constructor() { }

  choice = 'map';

  public showMap(): void{
    this.choice = 'map';
  }

  public showForm(): void{
    this.choice = 'location';
  }

  ngOnInit(): void {

  }
}
