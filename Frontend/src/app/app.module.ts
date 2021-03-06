import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {AgmCoreModule} from '@agm/core';
import {GooglePlaceModule} from 'ngx-google-places-autocomplete';

import { AppComponent } from './app.component';
import { LoginComponent } from './modules/Auth/login/login.component';
import { NotFoundComponent } from './ui/not-found/not-found.component';
import { RegisterComponent } from './modules/Auth/register/register.component';
import { HeaderComponent } from './ui/header/header.component';
import { MainComponent } from './shared/components/main/main.component';
import { EditUserComponent } from './modules/user/edituser/edit-user.component';
import { AdminScreenComponent } from './modules/admin/components/admin-screen/admin-screen.component';
import { UsersTableComponent } from './modules/admin/components/users-table/users-table.component';
import { EdituserByAdminComponent } from './modules/admin/components/edituser-by-admin/edituser-by-admin.component';
import { CreateuserComponent } from './modules/admin/components/createuser/createuser.component';
import { UserinfoComponent } from './modules/admin/components/userinfo/userinfo.component';
import {AdminAccessGuard} from './shared/guards/admin-access-guard.service';
import {TokenInterceptor} from './shared/TokenInterceptor';
import { AboutComponent } from './ui/about/about.component';
import { MainCarouselComponent } from './ui/main-carousel/main-carousel.component';
import { CreateCarComponent } from './modules/cars/create-car/create-car.component';
import { CarTableComponent } from './modules/cars/car-table/car-table.component';
import { CarManagementScreenComponent } from './modules/cars/car-management-screen/car-management-screen.component';
import { CarCardComponent } from './modules/cars/car-card/car-card.component';
import { CarInfoComponent } from './modules/cars/car-info/car-info.component';
import { CarTechDescriptionComponent } from './modules/cars/car-tech-description/car-tech-description.component';
import { PhotoAutoComponent } from './modules/cars/photo-auto/photo-auto.component';
import { RentalConditionsComponent } from './ui/rental-conditions/rental-conditions.component';
import { CarPhotoComponent } from './modules/cars/car-photo/car-photo.component';
import { CarPricesComponent } from './modules/cars/car-prices/car-prices.component';
import { CarEditComponent } from './modules/cars/car-edit/car-edit.component';
import { AutoparkComponent } from './modules/cars/autopark/autopark.component';
import { CarCardSmallComponent } from './modules/cars/car-card-small/car-card-small.component';
import { CarImagesManagementComponent } from './modules/cars/car-images-management/car-images-management.component';
import { HeadImageComponent } from './ui/head-image/head-image.component';
import { RentalPointsManagementComponent } from './modules/renatlPoints/rental-points-management/rental-points-management.component';
import { RentalPointCreateComponent } from './modules/renatlPoints/rental-point-create/rental-point-create.component';
import { FilterArrayPipe } from './shared/pipes/filter-countries.pipe';
import { CreateRentalPointByLocationComponent } from './modules/renatlPoints/create-rental-point-by-location/create-rental-point-by-location.component';
import { RentalPointTableComponent } from './modules/renatlPoints/rental-point-table/rental-point-table.component';
import { RentalInfoComponent } from './modules/renatlPoints/rental-info/rental-info.component';
import { RentalEditComponent } from './modules/renatlPoints/rental-edit/rental-edit.component';
import { MapMarkersComponent } from './modules/maps/map-markers/map-markers.component';
import { MapOneMarkerComponent } from './modules/maps/map-one-marker/map-one-marker.component';
import { RentalPointsMapComponent } from './modules/renatlPoints/rental-points-map/rental-points-map.component';
import { MapPointsComponent } from './modules/maps/map-points/map-points.component';
import { BookingFlowComponent } from './modules/booking/booking-flow/booking-flow.component';
import { LoginModalComponent } from './modules/Auth/login-modal/login-modal.component';
import { LoginFormComponent } from './modules/Auth/login-form/login-form.component';
import { BookingCardComponent } from './modules/booking/booking-card/booking-card.component';
import { BookingManagementComponent } from './modules/booking/booking-management/booking-management.component';
import { FilterBarComponent } from './modules/cars/filter-bar/filter-bar.component';
import { UserInfoModalComponent } from './modules/admin/components/user-info-modal/user-info-modal.component';
import { EditUserModalComponent } from './modules/admin/components/edit-user-modal/edit-user-modal.component';
import { ChatComponent } from './modules/chatpage/chat/chat.component';
import { SentMessageComponent } from './modules/chatpage/sent-message/sent-message.component';
import { ReceivedMessageComponent } from './modules/chatpage/received-message/received-message.component';
import { MessageTypingBoxComponent } from './modules/chatpage/message-typing-box/message-typing-box.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NotFoundComponent,
    RegisterComponent,
    HeaderComponent,
    MainComponent,
    EditUserComponent,
    AdminScreenComponent,
    UsersTableComponent,
    EdituserByAdminComponent,
    CreateuserComponent,
    UserinfoComponent,
    AboutComponent,
    MainCarouselComponent,
    CreateCarComponent,
    CarTableComponent,
    CarManagementScreenComponent,
    CarCardComponent,
    CarInfoComponent,
    CarTechDescriptionComponent,
    PhotoAutoComponent,
    RentalConditionsComponent,
    CarPhotoComponent,
    CarPricesComponent,
    CarEditComponent,
    AutoparkComponent,
    CarCardSmallComponent,
    CarImagesManagementComponent,
    HeadImageComponent,
    RentalPointsManagementComponent,
    RentalPointCreateComponent,
    FilterArrayPipe,
    CreateRentalPointByLocationComponent,
    RentalPointTableComponent,
    RentalInfoComponent,
    RentalEditComponent,
    MapMarkersComponent,
    MapOneMarkerComponent,
    RentalPointsMapComponent,
    MapPointsComponent,
    BookingFlowComponent,
    LoginModalComponent,
    LoginFormComponent,
    BookingCardComponent,
    BookingManagementComponent,
    FilterBarComponent,
    UserInfoModalComponent,
    EditUserModalComponent,
    ChatComponent,
    SentMessageComponent,
    ReceivedMessageComponent,
    MessageTypingBoxComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyDLUeOZwMQ6QivFjbHBuauXjQAr6fYGpIQ'
    }),
    GooglePlaceModule
  ],
  providers: [AdminAccessGuard, {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
