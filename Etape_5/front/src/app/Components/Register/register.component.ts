import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
 
  error_message: string = "";

  constructor() {}
  
  onSubmit(form: NgForm) {
    if (form.valid != true) {
      this.error_message = "nom d'utilisateur et mot de passe requis"
    } else {
      //appel HTTP
    }
  }
}
