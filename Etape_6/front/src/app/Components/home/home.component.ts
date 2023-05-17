import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
// import { Post } from 'src/app/Models/Post';
// import { PostService } from 'src/app/Services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{
  // posts: Post[] = [];

  constructor (
    // protected _postService: PostService,
    protected _router: Router
  ) { }

  ngOnInit() {
    // this._postService.getAll().subscribe(
    //   data => {
    //     for (const post of data) {
    //       this.posts.push(post);
    //     }
    //   },
    //   err => {
    //     console.log("erreur");
    //   }
    // )
  }
}
