import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './Services/auth.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'front';

  constructor(
    private _router: Router,
    protected _authService: AuthService
  ) {

  }

  goToRegister() {
    this._router.navigate(['register']);
  }
  goToLogin() {
    this._router.navigate(['login']);
  }
  goToHome() {
    this._router.navigate(['']);
  }
  Logout() {
    this._authService.logout();
  }
}
