import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Tag } from 'src/app/Models/Tag';
import { User } from 'src/app/Models/User';
import { TagService } from 'src/app/Services/tag.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-edition',
  templateUrl: './edition.component.html',
  styleUrls: ['./edition.component.scss']
})
export class EditionComponent implements OnInit {
  modelEdition: string = '';
  user: User | null = null;
  tag: Tag | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    protected _userService: UserService,
    protected _tagService: TagService
  ) {}

  ngOnInit(): void {
    this.modelEdition = this.route.snapshot.url[1].path;
    const id = this.route.snapshot.url[3].path;
    if (id) {
      if (this.modelEdition === 'user') {
        this._userService.getOne(+id).subscribe(
          data => {
            this.user = data;
          }
        )
      } else {
        this._tagService.getOne(+id).subscribe(
          data => {
            this.tag = data;
          }
        )
      }
    }
  }

  submit(form: NgForm) {
    const id = this.route.snapshot.url[3].path;
    if (form.valid) {
      if (this.modelEdition === 'user') {
        this._userService.update(form.value, +id).subscribe(
          data => {
            this.router.navigate(['admin']);
          }
        )
      } else {
        this._tagService.update(form.value, +id).subscribe(
          data => {
            this.router.navigate(['admin']);
          }
        )
      }
    }
  }

  goTo(path: string, id: number | null = null) {
    if (id)
      this.router.navigate([path, id]);
    else 
      this.router.navigate([path]);
  }
}
