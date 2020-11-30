import {Component, OnInit, ViewChild} from '@angular/core';
import {CarForSmallCard} from '../../../shared/utils/Car/CarForSmallCard';
import {CarService} from '../../../shared/services/car.service';
import {catchError, first, map, tap} from 'rxjs/operators';
import {ActivatedRoute} from '@angular/router';
import {Observable, of} from 'rxjs';
import {RentalPointService} from '../../../shared/services/rental-point.service';
import {HttpResponseService} from '../../../shared/services/http-response.service';
import {UserInfoService} from '../../../shared/services/user-info.service';
import {AuthorizeService} from '../../../shared/services/authorize.service';
import {LoginRequest} from '../../../shared/utils/Authorize/LoginRequest';
import {BookingFlowComponent} from '../booking-flow/booking-flow.component';
import {LoginModalComponent} from '../../Auth/login-modal/login-modal.component';
import {BookingRequest} from '../../../shared/utils/Car/BookingRequest';

@Component({
  selector: 'app-autopark',
  templateUrl: './autopark.component.html',
  styleUrls: ['./autopark.component.css']
})
export class AutoparkComponent implements OnInit {

  constructor(private carService: CarService,
              private rentalPointService: RentalPointService,
              private route: ActivatedRoute,
              private httpResponseService: HttpResponseService,
              private userInfoService: UserInfoService,
              private authorizeService: AuthorizeService) {}

  @ViewChild(BookingFlowComponent, {static: false})
  private bookingFlowComponent: BookingFlowComponent;

  @ViewChild(LoginModalComponent, {static: false})
  private loginModalComponent: LoginModalComponent;

  chosenCarIndex: number;
  cars: CarForSmallCard[];
  cars$: Observable<Array<CarForSmallCard>>;
  private rentalPointId: string;
  rentalPointName$: Observable<string>;

  showModalForBooking(index: number): void{
    this.chosenCarIndex = index;
    if (!this.authorizeService.isAuthorized){
      this.loginModalComponent.showModal();
   }else{
      this.showBookingFlow(this.chosenCarIndex);
    }

  }
  showBookingFlow(index): void{
    this.bookingFlowComponent.showModal();
    this.bookingFlowComponent.imageSrc = this.cars[index]?.imageName;
    this.bookingFlowComponent.carName = this.cars[index]?.name;
    this.bookingFlowComponent.bookingRequest.carId = this.cars[index]?.id;
    this.userInfoService.getUser(this.authorizeService.userId).subscribe(
      user => {
        for (const key in user){
          if (key in this.bookingFlowComponent.bookingRequest){
            this.bookingFlowComponent.bookingRequest[key] = user[key];
          }
          this.bookingFlowComponent.bookingRequest.userId = user.id;
        }
        console.log(this.bookingFlowComponent.bookingRequest);
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }

  login(loginRequest: LoginRequest): void{
    this.authorizeService.login(loginRequest).subscribe(
      data => {
        this.loginModalComponent.closeModal();
        this.showBookingFlow(this.chosenCarIndex);
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }

  bookCar(request: BookingRequest): void{
    console.log(request);
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.rentalPointId = params.pointid ?? '';
      this.cars$ = this.rentalPointService.getRentalPointCars(this.rentalPointId).pipe(
      map(cars => {
        cars?.forEach(car => {
          if (car.imageName) {
            car.imageName = this.carService.backendUrlForImages + car.imageName;
          }
        });
        return cars;
      }),
      tap(data => this.cars = data),
      catchError(err  => {
        this.httpResponseService.showErrorMessage(err);
        return of(err);
      })
    );
      if (this.rentalPointId){
        this.rentalPointName$ = this.rentalPointService.getRentalPointsNames(this.rentalPointId).pipe(
          first(),
          catchError(err  => {
            this.httpResponseService.showErrorMessage(err);
            return of(err);
          })
        );
      }
      else {
        this.rentalPointName$ = null;
      }
    });
  }
}
