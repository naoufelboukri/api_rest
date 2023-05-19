import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { AuthenticationComponent } from './Components/authentication/authentication.component';
import { PageNotFoundComponent } from './Components/Others/page-not-found/page-not-found.component';
import { UnauthorizeComponent } from './Components/Others/unauthorize/unauthorize.component';
import { RegisterComponent } from './Components/register/register.component';
import { CustomerGuard } from './Guards/customer.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'auth', component: AuthenticationComponent, canActivate: [CustomerGuard]},
  { path: 'register', component: RegisterComponent, canActivate: [CustomerGuard]},
  { path: 'unauthorize', component: UnauthorizeComponent},
  { path: '**', component: PageNotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
