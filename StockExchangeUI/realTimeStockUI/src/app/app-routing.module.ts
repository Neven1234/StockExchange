import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Pages/home/home/home.component';
import { RegisterComponent } from './Pages/User/register/register.component';
import { LoginComponent } from './Pages/User/login/login.component';
import { authGuardGuard } from './Core/gurd/auth-guard.guard';
import path from 'path';
import { OrderComponent } from './Pages/Orders/order/order.component';

const routes: Routes = [
  {
    path:'',
    component:HomeComponent
  },
  {
    path:'register',
    component:RegisterComponent,
    
  },
  {
    path:'Login',
    component:LoginComponent
  },
  {
    path:'order',
    component:OrderComponent,
    canActivate:[authGuardGuard]
  }

 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
