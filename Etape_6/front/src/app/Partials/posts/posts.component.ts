import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Post } from 'src/app/Models/Post';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { PostService } from 'src/app/Services/post.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent {
  posts: Post[] = [];

  constructor(
    private _route: Router,
    protected _postService: PostService,
    protected _authService: AuthenticationService
  ) { }

  ngOnInit(): void {
    this._postService.getAll().subscribe(
      data => {
        for (const post of data) {
          this.posts.push(post);
        }
      }
    )
  }

  goTo(route: string, id: number | null = null) {
    if (id) {
      this._route.navigate([route, id]);
    } else {
      this._route.navigate([route]);
    }
  }

  getDate(date: Date) {
    const DATE: Date = new Date(date);
    const month = DATE.getMonth() + 1;
    return DATE.getDate() + '/' + (month < 10 ? "0" + month : month) + '/' + DATE.getFullYear();
  }
}