import { Component, OnInit } from '@angular/core';
import {RentalPointService} from '../../../shared/services/rental-point.service';
import {RentalPointTableInfo} from '../../../shared/utils/rentalPoint/RentalPointTableInfo';

@Component({
  selector: 'app-rental-points-management',
  templateUrl: './rental-points-management.component.html',
  styleUrls: ['./rental-points-management.component.css']
})
export class RentalPointsManagementComponent implements OnInit {

  constructor(private rentalPointService: RentalPointService) {}

  pointsForTable: Array<RentalPointTableInfo>;

  ngOnInit(): void {
  }

}
