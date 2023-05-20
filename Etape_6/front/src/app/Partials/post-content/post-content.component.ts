import { Component, Input, OnChanges } from '@angular/core';
import { marked } from 'marked';

@Component({
  selector: 'app-post-content',
  template: `
    <div [innerHTML]="convertedData"></div>
  `,
  styleUrls: ['./post-content.component.scss']
})
export class PostContentComponent implements OnChanges{
  @Input('data') data: string;
  convertedData: string;

  ngOnChanges() {
    var md = marked.setOptions({
      gfm: true,
      breaks: true,
    });
    this.convertedData = md.parse(this.data);
  }
}
