import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  isValid: boolean = true;

  public constructor(
    private _router: Router,
    private _authService: AuthService
  ) { }

  login(username: string, password: string) {
    this._authService.login(username, password).subscribe(
      (data) => {
        this.isValid = true;
        localStorage.setItem('UserToken', data.toString());
        window.location.reload();
      },
      (err) => {
        this.isValid = false;
      }
    )
  }

  goToRegister() {
    this._router.navigate(['register']);
  }
}
