import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/Services/authentication.service';
@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss']
})
export class AuthenticationComponent {

  isValid: boolean = true;

  constructor(
    private _authService: AuthenticationService,
    private _router: Router
  ){}

  login(f: NgForm) {
    this._authService.login(f.value.username, f.value.password).subscribe(
      data => {
        this.isValid = true;
        localStorage.setItem('UserToken', data.token.toString());
        this._router.navigate(['home']);
        window.location.reload();
      },
      err => {
        this.isValid = false;
      }
    )
  }
}
