import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './modules/Auth/login/login.component';
import {NotFoundComponent} from './ui/not-found/not-found.component';
import {RegisterComponent} from './modules/Auth/register/register.component';
import {MainComponent} from './shared/components/main/main.component';
import {EditUserComponent} from './modules/user/edituser/edit-user.component';
import {AdminScreenComponent} from './modules/admin/components/admin-screen/admin-screen.component';
import {EdituserByAdminComponent} from './modules/admin/components/edituser-by-admin/edituser-by-admin.component';
import {CreateuserComponent} from './modules/admin/components/createuser/createuser.component';
import {UserinfoComponent} from './modules/admin/components/userinfo/userinfo.component';
import {AdminAccessGuard} from './shared/guards/admin-access-guard.service';
import {AboutComponent} from './ui/about/about.component';
import {CreateCarComponent} from './modules/cars/create-car/create-car.component';
import {CarManagementAccessGuard} from './shared/guards/car-management-access.guard';
import {CarManagementScreenComponent} from './modules/cars/car-management-screen/car-management-screen.component';
import {CarInfoComponent} from './modules/cars/car-info/car-info.component';
import {CarEditComponent} from './modules/cars/car-edit/car-edit.component';
import {CarImagesManagementComponent} from './modules/cars/car-images-management/car-images-management.component';

const routes: Routes = [
  { path: '', component: MainComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'edit/:id', component: EditUserComponent},
  { path: 'adminpage', component: AdminScreenComponent, canActivate: [AdminAccessGuard]},
  { path: 'admin/edituser/:id', component: EdituserByAdminComponent, canActivate: [AdminAccessGuard]},
  { path: 'admin/createuser', component: CreateuserComponent, canActivate: [AdminAccessGuard]},
  { path: 'userinfo/:id', component: UserinfoComponent, canActivate: [AdminAccessGuard]},
  { path: 'about', component: AboutComponent},
  { path: 'createcar', component: CreateCarComponent, canActivate: [CarManagementAccessGuard]},
  { path: 'carmanagement', component: CarManagementScreenComponent, canActivate: [CarManagementAccessGuard]},
  { path: 'carinfo/:id', component: CarInfoComponent},
  { path: 'caredit/:id', component: CarEditComponent, canActivate: [CarManagementAccessGuard]},
  { path: 'careditimages/:id', component: CarImagesManagementComponent, canActivate: [CarManagementAccessGuard]},
  { path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
