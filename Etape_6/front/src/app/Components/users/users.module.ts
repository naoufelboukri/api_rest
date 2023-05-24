import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilComponent } from './profile/profil.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/Guards/auth.guard';
import { AdminGuard } from 'src/app/Guards/admin.guard';
import { BackOfficeComponent } from './back-office/back-office.component';
import { TableComponent } from 'src/app/Partials/table/table.component';
import { myFormComponent } from '../../Partials/form/form.component';
import { FormsModule } from '@angular/forms';
import { EditionComponent } from './edition/edition.component';

const routesUsers: Routes = [
  { path: 'profil', component: ProfilComponent, canActivate: [AuthGuard] },
  { path: 'admin', component: BackOfficeComponent, canActivate: [AdminGuard] },
  { path: 'admin/user/edit/:id', component: EditionComponent, canActivate: [AdminGuard] },
  { path: 'admin/tag/edit/:id', component: EditionComponent, canActivate: [AdminGuard] },

]

@NgModule({
  declarations: [
    ProfilComponent,
    BackOfficeComponent,
    TableComponent,
    myFormComponent,
    EditionComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routesUsers)
  ],
})
export class UsersModule { }
