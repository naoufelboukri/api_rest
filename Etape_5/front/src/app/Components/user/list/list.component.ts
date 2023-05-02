import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/User';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  users: User[] = [];
  isAdmin: boolean = false;

  constructor (
    protected _userService: UserService,
    protected _authService: AuthService,
    private _router: Router,
  ) { 

  }

  ngOnInit() {

    this._userService.me().subscribe(
      data => {
        if (data.role === 'ROLE_ADMIN') {
          this.isAdmin = true;
        }
      }
    )

    this._userService.getUsers().subscribe(
      data => {
        for (const user of data) {
          this.users.push(user);
        }
      }
    )
  }

  goToUser(user: User) {
    console.log(user);
    // this._router.navigate(['user', user.id]);
  }
}
