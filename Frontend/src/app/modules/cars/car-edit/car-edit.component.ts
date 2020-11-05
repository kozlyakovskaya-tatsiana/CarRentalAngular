import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-car-edit',
  templateUrl: './car-edit.component.html',
  styleUrls: ['./car-edit.component.css']
})
export class CarEditComponent implements OnInit {

  constructor(private activateRoute: ActivatedRoute) {
    this.carEditId = activateRoute.snapshot.params.id;
  }

  carEditId: string;

  ngOnInit(): void {
  }

}
