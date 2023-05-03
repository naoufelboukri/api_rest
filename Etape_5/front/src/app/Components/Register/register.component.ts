import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
 
  error_message: string = "";

  constructor(
    private _auth: AuthService,
    private _router: Router
  ) {}
  
  onSubmit(form: NgForm) {
    if (form.valid != true) {
      this.error_message = "nom d'utilisateur et mot de passe requis";
    } else {
      this._auth.register(form.value.username, form.value.password)
      .subscribe((data) => {
        this._router.navigate(['']);
      },
      (exception) => {
        this.error_message = exception.error.message;
      });
    }
  }

  goToLogin() {
    this._router.navigate(['login']);
  }
}
