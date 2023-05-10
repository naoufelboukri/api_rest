import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../Components/Login/login.component';
import { RegisterComponent } from '../Components/Register/register.component';
import { ErrorComponent } from '../Components/error/error.component';
import { HomeComponent } from '../Components/home/home.component';
import { CustomerGuard } from '../Guards/customer.guard';
import { PageNotFoundComponent } from '../Components/page-not-found/page-not-found.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, canActivate: [CustomerGuard]},
  { path: 'register', component: RegisterComponent, canActivate: [CustomerGuard]},
  { path: 'error', component: ErrorComponent},
  { path: '', component: HomeComponent },
  { path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
