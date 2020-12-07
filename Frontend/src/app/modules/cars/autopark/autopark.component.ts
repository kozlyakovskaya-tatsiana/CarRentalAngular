import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
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
import {BookingFlowComponent} from '../../booking/booking-flow/booking-flow.component';
import {LoginModalComponent} from '../../Auth/login-modal/login-modal.component';
import {BookingRequest} from '../../../shared/utils/booking/BookingRequest';
import {BookingService} from '../../../shared/services/booking.service';
import swal from 'sweetalert2';
import {CountyInfo} from '../../../shared/utils/filters/CountyInfo';
import {CityInfo} from '../../../shared/utils/filters/CityInfo';
import {FilterBarComponent} from '../filter-bar/filter-bar.component';
import {CarFilter} from '../../../shared/utils/filters/CarFilter';
import {PagedResponse} from '../../../shared/utils/filters/PagedResponse';

@Component({
  selector: 'app-autopark',
  templateUrl: './autopark.component.html',
  styleUrls: ['./autopark.component.css']
})
export class AutoparkComponent implements OnInit, AfterViewInit {

  constructor(private carService: CarService,
              private rentalPointService: RentalPointService,
              private route: ActivatedRoute,
              private httpResponseService: HttpResponseService,
              private userInfoService: UserInfoService,
              private authorizeService: AuthorizeService,
              private bookingService: BookingService) {
    this.pagedResponse = new PagedResponse<CarForSmallCard>();
    this.pagedResponse.pageNumber = 1;
    this.pageSize = 2;
  }

  public pageSize: number;

  @ViewChild(BookingFlowComponent, {static: false})
  private bookingFlowComponent: BookingFlowComponent;

  @ViewChild(LoginModalComponent, {static: false})
  private loginModalComponent: LoginModalComponent;

  @ViewChild(FilterBarComponent, {static: false})
  private filterBarComponent: FilterBarComponent;

  public pagedResponse: PagedResponse<CarForSmallCard>;

  chosenCarIndex: number;
  cars: CarForSmallCard[];
  cars$: Observable<Array<CarForSmallCard>>;
  private rentalPointId: string;
  rentalPointName$: Observable<string>;

  countries: Array<CountyInfo>;
  cities: Array<CityInfo>;
  marks: Array<string>;
  transmissions: Array<string>;

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
    this.bookingFlowComponent.carCostPerDay = this.cars[index]?.costPerDay;
    this.bookingFlowComponent.imageSrc = this.cars[index]?.imageName;
    this.bookingFlowComponent.carName = this.cars[index]?.name;
    this.bookingFlowComponent.bookingRequest.carId = this.cars[index]?.id;
    this.userInfoService.getUser(this.authorizeService.userId).subscribe(
      user => {
        this.bookingFlowComponent.bookingRequest.personName = user.name;
        this.bookingFlowComponent.bookingRequest.personSurname = user.surname;
        this.bookingFlowComponent.bookingRequest.personPassportId = user.passportId;
        this.bookingFlowComponent.bookingRequest.personPassportSerialNumber = user.passportSerialNumber;
        this.bookingFlowComponent.bookingRequest.personDateOfBirth = user.dateOfBirth;
        this.bookingFlowComponent.bookingRequest.personPhoneNumber = user.phoneNumber;
        this.bookingFlowComponent.bookingRequest.userId = user.id;
        this.bookingFlowComponent.bookingRequest.carId = this.cars[index]?.id;
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
    this.bookingService.bookCar(this.bookingFlowComponent.bookingRequest).subscribe(
      data => {
        console.log(data);
        swal.fire({
          title: 'You request is accepted',
          icon: 'success'
        }).then(val => {
          window.location.reload();
        });
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }

  getCountries(): void{
    this.carService.getCarsCountries().subscribe(
      data => {
        this.countries = data;
        console.log(this.countries);
      },
       err => {
        this.httpResponseService.showErrorMessage(err);
       }
    );
  }

  getCities(countryId: string): void{
    this.carService.getCarsCities(countryId).subscribe(
      data => {
        this.cities = data;
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }

  getMarks(): void{
    this.carService.getCarsMarks().subscribe(
      data => {
        this.marks = data;
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }

  onCountryChanged(countryId: string): void{
      this.carService.getCarsCities(countryId).subscribe(
        data => {
          this.filterBarComponent.cities = data;
        },
        err => {
          this.httpResponseService.showErrorMessage(err);
        }
      );
  }

  onCityChanged(cityId: string): void{
    this.carService.getCarsPoints(cityId).subscribe(
      data => {
        console.log(data);
        this.filterBarComponent.rentalPoints = data;
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }

  filterCars(filter: CarFilter): void{
    filter.pageSize = this.pageSize;
    filter.pageNumber = 1;
    console.log(filter);
    this.carService.filterAndPaginateCars(filter).pipe(
      tap(data => {
        console.log(data);
      }),
      map(data => {
         data.itemsPerPage.forEach(car => {
          if (car.imageName) {
            car.imageName = this.carService.backendUrlForImages + car.imageName;
          }
        });
         return data;
      })
    ).subscribe(data => {
      this.pagedResponse = data;
    },
      err => {
      this.httpResponseService.showErrorMessage(err);
    });
  }

  loadMoreCars(): void{
    this.filterBarComponent.carFilter.pageNumber += 1;
    console.log(this.filterBarComponent.carFilter.pageNumber);
    this.carService.filterAndPaginateCars(this.filterBarComponent.carFilter).pipe(
      tap(data => {
        console.log(data);
      }),
      map(data => {
        data.itemsPerPage.forEach(car => {
          if (car.imageName) {
            car.imageName = this.carService.backendUrlForImages + car.imageName;
          }
        });
        return data;
      })
    ).subscribe(data => {
        this.pagedResponse.itemsPerPage.push(...data.itemsPerPage);
        const cars = this.pagedResponse.itemsPerPage;
        this.pagedResponse = data;
        this.pagedResponse.itemsPerPage = cars;
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      });
  }

  setPageNumber(event, pageSize: number): void{
    event.preventDefault();
    Array.from(document.getElementsByClassName('cars-per-page')).forEach(el => {
        el.className = el.className.replace('underline', '');
    });
    event.target.className += ' underline ';

    this.pageSize = pageSize;

    this.filterCars(this.filterBarComponent.carFilter);
  }

  ngAfterViewInit(): void {
    this.route.queryParams.subscribe(params => {
      this.filterBarComponent.carFilter.rentalPointId = params.pointid ?? '';
      console.log(params.pointid);
      this.filterCars(this.filterBarComponent.carFilter);
    });
    this.getCountries();
    this.getMarks();
    this.carService.getTransmissionsTypes().subscribe(
      data => {
        this.transmissions = data;
        console.log(data);
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
    this.carService.getCarcases().subscribe(
      data => {
        this.filterBarComponent.carcases = data;
      },
      err => {
        this.httpResponseService.showErrorMessage(err);
      }
    );
  }

  ngOnInit(): void {

  }
}
