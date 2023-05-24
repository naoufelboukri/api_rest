import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthComponent } from './auth/auth.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CustomerGuard } from 'src/app/Guards/customer.guard';

const routes: Routes = [
  { path: 'authentication', component: AuthComponent, canActivate: [CustomerGuard] },
  { path: 'inscription', component: AuthComponent, canActivate: [CustomerGuard] },
]

@NgModule({
  declarations: [
    AuthComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class AuthenticationModule { }
