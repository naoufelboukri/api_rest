import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { AuthGuard } from 'src/app/Guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { DetailComponent } from './detail/detail.component';
import { MonCompteComponent } from './mon-compte/mon-compte.component';
import { EditComponent } from './edit/edit.component';
import { FormsModule } from '@angular/forms';

const userRoutes: Routes = [
  { path: 'users', component: ListComponent, canActivate: [AuthGuard] },
  { path: 'user/:id', component: DetailComponent, canActivate: [AuthGuard] },
  { path: 'user/edit/:id', component: EditComponent, canActivate: [AuthGuard] },
  { path: 'mon-compte', component: MonCompteComponent, canActivate: [AuthGuard] },
]

@NgModule({
  declarations: [
    ListComponent,
    DetailComponent,
    MonCompteComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(userRoutes)
  ]
})
export class UserModule { }
