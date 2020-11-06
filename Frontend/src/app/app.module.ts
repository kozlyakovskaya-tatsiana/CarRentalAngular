import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './modules/Auth/login/login.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
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
    CarPricesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [AdminAccessGuard, {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
