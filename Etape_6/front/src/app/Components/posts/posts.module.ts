import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DetailComponent } from './detail/detail.component';
import { ListComponent } from './list/list.component';
import { NewComponent } from './new/new.component';
import { AuthGuard } from 'src/app/Guards/auth.guard';

const routesPost: Routes = [
  { path: 'post/new', component: NewComponent, canActivate: [AuthGuard] },
  { path: 'post/:id', component: DetailComponent },
  { path: 'posts', component: ListComponent },
]

@NgModule({
  declarations: [
    DetailComponent,
    ListComponent,
    NewComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routesPost)
  ]
})
export class PostsModule { }
