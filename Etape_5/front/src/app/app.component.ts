import { Component } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'front';

  constructor(
    private _router: Router
  ) {

  }

  goToRegister() {
    this._router.navigate(['register']);
  }
  goToLogin() {
    this._router.navigate(['login']);
  }
}
