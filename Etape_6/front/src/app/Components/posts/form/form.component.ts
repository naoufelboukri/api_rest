import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/Models/Post';
import { Tag } from 'src/app/Models/Tag';
import { PostService } from 'src/app/Services/post.service';
import { TagService } from 'src/app/Services/tag.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {

  tags: Tag[] = [];
  content: string = "";
  post: Post | null;

  constructor (
    private _tagService: TagService,
    private _postService: PostService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this._tagService.getAll().subscribe(
      data => {
        for (const tag of data.data) {
          this.tags.push(tag);
        }
      }
    )      


    if (this.route.snapshot.url[1].path === 'edit') {
      const postId = this.route.snapshot.paramMap.get('id');
      if (postId) {
        this._postService.getOne(+postId).subscribe(
          data => {
            this.post = data;
          }
        )
      }
    }
    
  }

  save(form: NgForm) {
    let tags = form.value.tag;
    if (typeof tags !== 'string') {      
      tags = tags.includes(0) ? '' : tags.toString();
    }

    if(this.post) {
      // const object: any = {
      //   title: form.value.title,
      //   tags: tags,
      //   content: form.value.content
      // }

      const object: any = {};
      for (const value of Object.entries(form.value)) {
        if (value[1] !== "") {
          object[value[0]] = value[1];
        }
      }
      this._postService.update(object, this.post.id).subscribe(data => this.router.navigate(['profil']));
    } else {
      const post = new Post();
      post.content = this.content;
      post.title = form.value.title;
      post.tags = tags;
      this._postService.create(post).subscribe(
        data => {
          this.router.navigate(['posts']);
        }
        );
      }
    }
}
