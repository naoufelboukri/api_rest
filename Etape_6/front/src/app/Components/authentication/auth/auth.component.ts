import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/Services/authentication.service';
@Component({
  selector: 'app-authentication',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit{

  isValid: boolean = true;
  errorMessage: string = '';
  registering: boolean;
  constructor(
    private _authService: AuthenticationService,
    private _route: ActivatedRoute,
    private _router: Router
  ){}

  ngOnInit(): void {   
    this.registering = this._route.snapshot.url[0].path === 'inscription';
  }

  submit(f: NgForm) {
    if (f.valid) {
      if (this.registering) {
        this._authService.register(f.value.username, f.value.password).subscribe(
          data => this._router.navigate(['authentication']),
          err => {
            this.isValid = false;
            this.errorMessage = err.error.message;
          }
        )
      } else {
        this._authService.login(f.value.username, f.value.password).subscribe(
          data => {            
            this.isValid = true;
            localStorage.setItem('UserToken', data.token.toString());
            this._router.navigate(['']);
            window.location.reload();
          },
          err => {
            console.log(err);
            this.isValid = false;
            this.errorMessage = err.error.message; 
          }
        )
      }
    } else {
      this.isValid = false;
      this.errorMessage = "Les champs sont obligatoires";
    }
    
  }
}
