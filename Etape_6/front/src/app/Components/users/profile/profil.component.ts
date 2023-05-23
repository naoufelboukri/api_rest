import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/Models/Post';
import { User } from 'src/app/Models/User';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { PostService } from 'src/app/Services/post.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profil.component.html',
  styleUrls: ['./profil.component.scss']
})
export class ProfilComponent implements OnInit {
  user: User | null;
  username: string | null = null;
  posts: Post[] = [];
  constructor (
    protected _authService: AuthenticationService,
    protected _userService: UserService,
    protected _postService: PostService,
    private _router: Router,
  ) { }

  ngOnInit(): void {
    this._authService.me().subscribe(
      data => {
        this.user = data;
        this.username = this.user.username;
        this._userService.getOne(this.user.id).subscribe(
          data => {
            for (const post of data.posts) {
              this.posts.push(post);
            }
          }
        )
      },
      err => {
        this._router.navigate([""]);
      }
    )
  }

  changeBy(initial: HTMLDivElement, updated: HTMLDivElement, caller: any) {
    const buttonFocused = document.querySelector('.focused') as HTMLElement;
    buttonFocused.classList.remove('focused');
    caller.target.classList.add('focused')
    initial.classList.remove('pageFocus');
    updated.classList.add('pageFocus');
  }

  goTo(route: string, id: number | null = null) {
    if (id) {
      this._router.navigate([route, id]);
    } else {
      this._router.navigate([route]);
    }
  }

  openModal(modal: HTMLDivElement) {
    modal.classList.add('modal_show');
  }

  closeModal(modal: HTMLDivElement) {
    modal.classList.remove('modal_show');
  }

  deletePost(post: Post) {
    this._postService.delete(post.id).subscribe(
      data => {
        window.location.reload();
      }
    )
  }

  editUser(form: NgForm) {
    if (form.valid) {
      if (this.user) {     
        console.log(form.valid);
        let object: any = {
          username: this.username
        }
        console.log(object);
        this._userService.update(object, this.user.id).subscribe(
          data => this._authService.logout()
        )
      }
    }
  }
}
