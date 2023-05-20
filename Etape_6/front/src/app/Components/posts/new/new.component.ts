import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Tag } from 'src/app/Models/Tag';
import { TagService } from 'src/app/Services/tag.service';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class NewComponent implements OnInit {

  tags: Tag[] = [];

  constructor (
    private _tagService: TagService
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

  }
}
