import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DetailComponent } from './detail/detail.component';
import { ListComponent } from './list/list.component';
import { FormComponent } from './form/form.component';
import { AuthGuard } from 'src/app/Guards/auth.guard';
import { PostsComponent } from 'src/app/Partials/posts/posts.component';
import { FormsModule } from '@angular/forms';
import { PostContentComponent } from 'src/app/Partials/post-content/post-content.component';

const routesPost: Routes = [
  { path: 'posts', component: ListComponent },
  { path: 'post/new', component: FormComponent, canActivate: [AuthGuard] },
  { path: 'post/edit/:id', component: FormComponent, canActivate: [AuthGuard] },
  { path: 'post/:id', component: DetailComponent },
]

@NgModule({
  declarations: [
    DetailComponent,
    ListComponent,
    FormComponent,
    PostsComponent,
    PostContentComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routesPost)
  ],
  exports: [
    PostsComponent
  ]
})
export class PostsModule { }
