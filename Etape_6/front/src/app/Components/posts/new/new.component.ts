import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Post } from 'src/app/Models/Post';
import { Tag } from 'src/app/Models/Tag';
import { PostService } from 'src/app/Services/post.service';
import { TagService } from 'src/app/Services/tag.service';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class NewComponent implements OnInit {

  tags: Tag[] = [];
  content: string = "";

  constructor (
    private _tagService: TagService,
    private _postService: PostService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this._tagService.getAll().subscribe(
      data => {
        for (const tag of data) {
          this.tags.push(tag);
        }
      }
    )
  }

  create(form: NgForm) {

    let tags = form.value.tag;
    if (typeof tags !== 'string') {      
      tags = tags.includes(0) ? '' : tags.toString();
    }

    const post = new Post();
    post.content = this.content;
    post.title = form.value.title;
    post.tags = tags;

    console.log("### 1");
    console.log(post.content);
    
    this._postService.create(post).subscribe(
      data => {
        this.router.navigate(['posts']);
      }
    );
  }
}
