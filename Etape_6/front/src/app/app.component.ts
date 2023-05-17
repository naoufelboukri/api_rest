import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent {
  title = 'front';

  constructor (
    private _router: Router
  ) { }

  goTo(page: string) {
    this._router.navigate([page]);
  }
}
