import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-my-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class myFormComponent {
  @Input('data') data: any;

  check(d: any) {
    console.log(d);
  }
}
