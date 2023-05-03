import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { AuthGuard } from 'src/app/Guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { DetailComponent } from './detail/detail.component';

const userRoutes: Routes = [
  { path: 'users', component: ListComponent, canActivate: [AuthGuard] },
  { path: 'user/:id', component: DetailComponent, canActivate: [AuthGuard] },
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
