import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DetailComponent } from './detail/detail.component';
import { ListComponent } from './list/list.component';
import { NewComponent } from './new/new.component';
import { AuthGuard } from 'src/app/Guards/auth.guard';
import { PostsComponent } from 'src/app/Partials/posts/posts.component';

const routesPost: Routes = [
  { path: 'posts', component: ListComponent },
  { path: 'post/new', component: NewComponent, canActivate: [AuthGuard] },
  { path: 'post/:id', component: DetailComponent },
]

@NgModule({
  declarations: [
    DetailComponent,
    ListComponent,
    NewComponent,
    PostsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routesPost)
  ],
  exports: [
    PostsComponent
  ]
})
export class PostsModule { }
