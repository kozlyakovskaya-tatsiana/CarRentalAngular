import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { RegisterComponent } from './components/register/register.component';
import { HeaderComponent } from './components/header/header.component';
import { MainComponent } from './components/main/main.component';
import { EditUserComponent } from './components/edituser/edit-user.component';
import { AdminScreenComponent } from './components/admin-screen/admin-screen.component';
import { UsersTableComponent } from './components/users-table/users-table.component';
import { EdituserByAdminComponent } from './components/edituser-by-admin/edituser-by-admin.component';
import { CreateuserComponent } from './components/createuser/createuser.component';
import { UserinfoComponent } from './components/userinfo/userinfo.component';
import {AdminAccessGuard} from './guards/admin-access-guard.service';
import {TokenInterceptor} from './services/TokenInterceptor';
import { AboutComponent } from './components/about/about.component';
import { MainCarouselComponent } from './components/main-carousel/main-carousel.component';
import { CreateCarComponent } from './components/create-car/create-car.component';
import { CarTableComponent } from './components/car-table/car-table.component';
import { CarManagementScreenComponent } from './components/car-management-screen/car-management-screen.component';
import { CarCardComponent } from './components/car-card/car-card.component';
import { CarInfoComponent } from './components/car-info/car-info.component';
import { CarTechDescriptionComponent } from './components/car-tech-description/car-tech-description.component';
import { PhotoAutoComponent } from './components/photo-auto/photo-auto.component';
import { RentalConditionsComponent } from './rental-conditions/rental-conditions.component';


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
    RentalConditionsComponent
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
