import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/Services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  constructor (
    private _authService: AuthenticationService,
    private _router: Router
  ) { }

  register(form: NgForm) {
    console.log('1');
    
    if (form.valid) {
      console.log(form.value);
      this._authService.register(form.value.username, form.value.password).subscribe(
        data => {
      console.log('3');

          this._router.navigate(['authentication']);
        }
      )
    }
  }

}
