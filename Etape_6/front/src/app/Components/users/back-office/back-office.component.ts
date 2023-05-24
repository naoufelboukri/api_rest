import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Meta } from 'src/app/Models/Meta';
import { Post } from 'src/app/Models/Post';
import { Tag } from 'src/app/Models/Tag';
import { User } from 'src/app/Models/User';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { PostService } from 'src/app/Services/post.service';
import { TagService } from 'src/app/Services/tag.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './back-office.component.html',
  styleUrls: ['./back-office.component.scss']
})
export class BackOfficeComponent implements OnInit {
  user: User | null;
  users: User[] = [];
  posts: Post[] = [];
  tags: Tag[] = [];
  metaPosts: Meta;
  metaUsers: Meta;
  metaTags: Meta;

  per_page: number = 10;
  constructor (
    protected _authService: AuthenticationService,
    protected _userService: UserService,
    protected _postService: PostService,
    protected _tagService: TagService,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this.setData();
  }

  selectCategory(category: HTMLDivElement, caller: any) {
    const buttonFocused = document.querySelector('.focused') as HTMLElement;
    buttonFocused.classList.remove('focused');
    caller.target.classList.add('focused')

    const item = document.querySelector('.pageFocus') as HTMLElement;
    item.classList.remove('pageFocus');
    category.classList.add('pageFocus');
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
      data => this.setData()
    )
  }

  addTag(form: NgForm) {
    if (form.valid) {
      let tag = new Tag();
      tag.name = form.value.name;
      this._tagService.create(tag).subscribe(
        data => this.setData()
      )
    }
  }

  public setData() {
    this.users = [];
    this.tags = [];
    this.posts = [];
    this._userService.getAll(1, this.per_page).subscribe(
      data => {
        this.metaUsers = data.meta;
        for (const user of data.data) {
          this.users.push(user);
        }
      }
    )

    this._postService.getAll(1, this.per_page).subscribe(
      data => {
        this.metaPosts = data.meta;
        for (const post of data.data) {
          this.posts.push(post);
        }
      }
    )

    this._tagService.getAll(1, this.per_page).subscribe(
      data => {
        this.metaTags = data.meta;
        for (const tag of data.data) {
          this.tags.push(tag);
        }
      }
    )
  }

  reloadPosts(next: boolean) {
    if ((next && this.metaPosts.hasNext) || (!next && this.metaPosts.hasPrevious)) {
      const pageNumber = next ? this.metaPosts.currentPage + 1 : this.metaPosts.currentPage - 1;
      this.posts = [];
      this._postService.getAll(pageNumber, this.per_page).subscribe(
        data => {
          this.metaPosts = data.meta;
          for (const post of data.data) {
            this.posts.push(post);
          }
        }
      );
    }
  }

  reloadTags(next: boolean) {
    if ((next && this.metaTags.hasNext) || (!next && this.metaTags.hasPrevious)) {
      const pageNumber = next ? this.metaTags.currentPage + 1 : this.metaTags.currentPage - 1;
      this.tags = [];
      this._tagService.getAll(pageNumber, this.per_page).subscribe(
        data => {
          this.metaTags = data.meta;
          for (const tag of data.data) {
            this.tags.push(tag);
          }
        }
      );
    }
  }

  reloadUsers(next: boolean) {
    if ((next && this.metaUsers.hasNext) || (!next && this.metaUsers.hasPrevious)) {
      const pageNumber = next ? this.metaUsers.currentPage + 1 : this.metaUsers.currentPage - 1;
      this.users = [];
      this._userService.getAll(pageNumber, this.per_page).subscribe(
        data => {
          this.metaUsers = data.meta;
          for (const user of data.data) {
            this.users.push(user);
          }
        }
      );
    }
  }
}
