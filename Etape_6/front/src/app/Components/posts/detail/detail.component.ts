import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MOCK_POSTS } from 'src/app/Mock/posts';
import { Post } from 'src/app/Models/Post';
import { Rating } from 'src/app/Models/Rating';
import { PostService } from 'src/app/Services/post.service';
import { RatingService } from 'src/app/Services/rating.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {

  post: Post | null = null;
  comments: Rating[] = [];

  textareaContent: string = '';
  fakePosts: Post[] = MOCK_POSTS;

  constructor (
    protected _postService: PostService,
    private _ratingService: RatingService,
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
          },
          err => {
            console.error(err);
          }
        )
      }
  }

  addComment(form: NgForm) {
    const div = document.querySelector('#stars') as HTMLElement; 
    const stars = Array.from(div.querySelectorAll('*')) as Element[];
    let value = 0; 

    for (const star of stars) {      
      if (star.classList.contains('gold')) {
        value += 1;
      }
    }
    
    const rating: Rating = new Rating();
    rating.content = form.value.myComment;
    rating.userId = 1;
    rating.value = value;
    
    if (this.post) {
      rating.postId = this.post?.id;
    }
    
    this._ratingService.create(rating).subscribe(
      data => {        
        // window.location.reload();
      }
    )
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

  generateImg(id: number) {
    if (id === 101) {
      return 'assets/imgs/comment-faire-des-bebes.jpg'
    } else if (id === 102) {
      return 'assets/imgs/singe.jpg';
    } else if (id === 103) {
      return 'assets/imgs/saut.jpg';
    }

    return "https://us.123rf.com/450wm/sdmix/sdmix1803/sdmix180300248/96848899-abstrait-d%C3%A9grad%C3%A9-de-couleur-grise-illustration-vectorielle.jpg";
  }

  openModal(modal: HTMLDivElement) {
    modal.classList.add("modal_show");
  }
  
  closeModal(modal: HTMLDivElement) {
    modal.classList.remove("modal_show");
  }
  
  openModalDelete(modal: HTMLDivElement) {
    modal.classList.add("modal_show");
  }
  
  closeModalDelete(modal: HTMLDivElement) {
    modal.classList.remove("modal_show");
  }

  deleteComment(rating: Rating) {
    this._ratingService.delete(rating.id).subscribe(
      data => {
        // window.location.reload();
      }
    )
  }

  rate(rating: number): void {
    const div = document.querySelector('#stars') as HTMLElement; 
    const stars = Array.from(div.querySelectorAll('*')) as Element[];
    for (const star in stars) {
      const index = +star + 1;
      if (+index <= rating) {
        stars[star].classList.add('gold');
      } else {
        stars[star].classList.remove('gold');
      }
    }
  }
}
