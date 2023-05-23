import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Meta } from 'src/app/Models/Meta';

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
  meta: Meta;
  pageSize = 5;
  constructor(
    private _route: Router,
    protected _postService: PostService,
    protected _authService: AuthenticationService
  ) { }

  ngOnInit(): void {
    const pageSize = this.pageSize;
    const pageNumber = 1;

    this.posts = [];
    this._postService.getAll(pageNumber, pageSize).subscribe(
      data => {
        this.meta = data.meta;
        console.log(data.data);
        
        for (const post of data.data) {
          this.posts.push(post);
        }
      }
    );
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

  search(input: HTMLInputElement) {
    this._postService.getBySearch(1, input.value).subscribe(
      data => {
        this.posts = [];
        for (const post of data) {
          this.posts.push(post);
        }
      }
    )
  }

  reloadPosts(next: boolean) {
    if ((next && this.meta.hasNext) || (!next && this.meta.hasPrevious)) {
      const pageNumber = next ? this.meta.currentPage + 1 : this.meta.currentPage - 1;
      const pageSize = this.pageSize;
      this.posts = [];
      this._postService.getAll(pageNumber, pageSize).subscribe(
        data => {
          this.meta = data.meta;
          for (const post of data.data) {
            this.posts.push(post);
          }
        }
      );
    }
  }
}