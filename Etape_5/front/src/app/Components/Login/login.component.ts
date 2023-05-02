import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  isValid: boolean = true;

  public constructor(
    private _router: Router,
    private _authService: AuthService
  ) { }

  login(email: string, password: string) {
    this._authService.login(email, password).subscribe(
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
