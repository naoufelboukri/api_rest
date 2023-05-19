import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/Models/Post';
import { Rating } from 'src/app/Models/Rating';
import { PostService } from 'src/app/Services/post.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {

  post: Post | null = null;
  comments: Rating[] = [];

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
            for (const rating of this.post.ratings) {
              this.comments.push(rating);
            }
            console.log(this.post);
          },
          err => {
            console.error(err);
          }
        )
      }
  }

  scored(score: number) {
  const empty = `<i class="fa-regular fa-star"></i>`;
  const full = `<i class="fa-solid fa-star"></i>`;
  let output: string = '';
  for (let i = 0; i < 5; i++) {
    if (score !== 0) {
      output += full;
      score -= 1;
    } else {
      output += empty;
    }
  }
    return output;
  }

}
