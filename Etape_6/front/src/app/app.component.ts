import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './Services/authentication.service';
import { User } from './Models/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit{
  title = 'front';
  user: User | null = null;
  constructor (
    private _router: Router,
    protected _authService: AuthenticationService,
  ) { }

  ngOnInit(): void {
      this._authService.me().subscribe(
        data => {
          this.user = data;
        }
      )
  }

  goTo(page: string) {
    this._router.navigate([page]);
  }
}
