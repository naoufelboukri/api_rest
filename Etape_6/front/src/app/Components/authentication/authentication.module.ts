import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthComponent } from './auth/auth.component';
import { RegisterComponent } from './register/register.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CustomerGuard } from 'src/app/Guards/customer.guard';

const routes: Routes = [
  { path: 'authentication', component: AuthComponent, canActivate: [CustomerGuard] },
  { path: 'inscription', component: RegisterComponent, canActivate: [CustomerGuard] },
]

@NgModule({
  declarations: [
    AuthComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class AuthenticationModule { }
