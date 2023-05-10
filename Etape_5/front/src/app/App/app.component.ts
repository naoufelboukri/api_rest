import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../Services/auth.service';


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
  goToProfile() {
    this._router.navigate(['mon-compte']);
  }
  goToUsers() {
    this._router.navigate(['users']);
  }
  Logout() {
    this._authService.logout();
    this._router.navigate(['']);
  }
}
