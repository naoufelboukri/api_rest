import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { AuthGuard } from 'src/app/Guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { DetailComponent } from './detail/detail.component';

const userRoutes: Routes = [
{ path: 'users', component: ListComponent, canActivate: [AuthGuard] },

  // { path: 'register', component: RegisterComponent, canActivate: [CustomerGuard] },
  // { path: 'login', component: LoginComponent, canActivate: [CustomerGuard] },
  // { path: 'profile/edit/:id', component: EditComponent, canActivate: [AuthGuard]},
  // { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard]},
  // { path: 'profile/products', component: MyProductsComponent, canActivate: [AuthGuard]},
  // { path: 'admin/product/edit/:id', component: EditProductComponent, canActivate: [AdminGuard]},
]

@NgModule({
  declarations: [
    ListComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(userRoutes)
  ]
})
export class UserModule { }
