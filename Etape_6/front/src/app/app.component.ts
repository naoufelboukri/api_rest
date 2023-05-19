import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './Services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent {
  title = 'front';

  constructor (
    private _router: Router,
    protected _authService: AuthenticationService
  ) { }

  goTo(page: string) {
    this._router.navigate([page]);
  }
}
