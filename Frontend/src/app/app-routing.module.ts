import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './components/login/login.component';
import {NotFoundComponent} from './components/not-found/not-found.component';
import {RegisterComponent} from './components/register/register.component';
import {MainComponent} from './components/main/main.component';
import {EditUserComponent} from './components/edituser/edit-user.component';
import {AdminScreenComponent} from './components/admin-screen/admin-screen.component';
import {EdituserByAdminComponent} from './components/edituser-by-admin/edituser-by-admin.component';
import {CreateuserComponent} from './components/createuser/createuser.component';
import {UserinfoComponent} from './components/userinfo/userinfo.component';
import {AdminAccessGuard} from './guards/admin-access-guard.service';

const routes: Routes = [
  { path: '', component: MainComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'edit/:id', component: EditUserComponent},
  { path: 'adminpage', component: AdminScreenComponent, canActivate: [AdminAccessGuard]},
  { path: 'admin/edituser/:id', component: EdituserByAdminComponent, canActivate: [AdminAccessGuard]},
  { path: 'admin/createuser', component: CreateuserComponent, canActivate: [AdminAccessGuard]},
  { path: 'userinfo/:id', component: UserinfoComponent, canActivate: [AdminAccessGuard]},
  { path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
