import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilComponent } from './profile/profil.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/Guards/auth.guard';

const routesUsers: Routes = [
  { path: 'profil', component: ProfilComponent, canActivate: [AuthGuard] },
]

@NgModule({
  declarations: [
    ProfilComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routesUsers)
  ]
})
export class UsersModule { }
