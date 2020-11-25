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
import {AdminManagerAccessGuard} from './shared/guards/admin-manager-access-guard.service';
import {CarManagementScreenComponent} from './modules/cars/car-management-screen/car-management-screen.component';
import {CarInfoComponent} from './modules/cars/car-info/car-info.component';
import {CarEditComponent} from './modules/cars/car-edit/car-edit.component';
import {CarImagesManagementComponent} from './modules/cars/car-images-management/car-images-management.component';
import {AutoparkComponent} from './modules/cars/autopark/autopark.component';
import {RentalPointsManagementComponent} from './modules/renatlPoints/rental-points-management/rental-points-management.component';
import {RentalPointCreateComponent} from './modules/renatlPoints/rental-point-create/rental-point-create.component';
import {RentalInfoComponent} from './modules/renatlPoints/rental-info/rental-info.component';
import {RentalEditComponent} from './modules/renatlPoints/rental-edit/rental-edit.component';

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
  { path: 'createcar', component: CreateCarComponent, canActivate: [AdminManagerAccessGuard]},
  { path: 'carmanagement', component: CarManagementScreenComponent, canActivate: [AdminManagerAccessGuard]},
  { path: 'carinfo/:id', component: CarInfoComponent},
  { path: 'caredit/:id', component: CarEditComponent, canActivate: [AdminManagerAccessGuard]},
  { path: 'careditimages/:id', component: CarImagesManagementComponent, canActivate: [AdminManagerAccessGuard]},
  { path: 'autopark', component: AutoparkComponent},
  { path: 'rentalpointsmanagement', component: RentalPointsManagementComponent, canActivate: [AdminManagerAccessGuard]},
  { path: 'createrentalpoint', component: RentalPointCreateComponent, canActivate: [AdminManagerAccessGuard]},
  { path: 'editrentalpoint/:id', component: RentalEditComponent, canActivate: [AdminManagerAccessGuard]},
  { path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
