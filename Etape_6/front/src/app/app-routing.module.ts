import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { AuthenticationComponent } from './Components/authentication/authentication.component';
import { PageNotFoundComponent } from './Components/page-not-found/page-not-found.component';
import { UnauthorizeComponent } from './Components/unauthorize/unauthorize.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'auth', component: AuthenticationComponent},
  { path: 'unauthorize', component: UnauthorizeComponent},
  { path: '**', component: PageNotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
