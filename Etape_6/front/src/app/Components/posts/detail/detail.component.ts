import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/Models/Post';
import { PostService } from 'src/app/Services/post.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {

  post: Post | null = null;

  constructor (
    protected _postService: PostService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
      const postId: string | null = this.route.snapshot.paramMap.get('id');

      if (postId) {
        this._postService.getOne(+postId).subscribe(
          data => {
            this.post = data;
            console.log(this.post);
          },
          err => {
            console.error(err);
          }
        )
      }
  }

}
