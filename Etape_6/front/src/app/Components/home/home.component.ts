import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Meta } from 'src/app/Models/Meta';
import { Post } from 'src/app/Models/Post';
import { PostService } from 'src/app/Services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{
  posts: Post[] = [];
  meta: Meta;
  constructor (
    protected _postService: PostService,
    protected _router: Router
  ) { }

  ngOnInit() {
    this._postService.getAll().subscribe(
      data => {
        this.meta = data.meta;
        for (const post of data.data) {
          this.posts.push(post);
        }
      },
      err => {
        console.error("erreur");
      }
    )
  }

  goTo(route: string, id: number | null = null) {
    if (id) {
      this._router.navigate([route, id]);
    } else {
      this._router.navigate([route]);
    }
  }

  getDate(date: Date) {
    const DATE: Date = new Date(date);
    const month = DATE.getMonth() + 1;
    return DATE.getDate() + '/' + (month < 10 ? "0" + month : month) + '/' + DATE.getFullYear();
  }

  generatePosts() {
    this._postService.generatePosts().subscribe(data=> {window.location.reload()});
  }

  destroyPosts() {
    this._postService.destroyPosts().subscribe(data=> {window.location.reload()});
  }
}
