import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Address } from 'src/app/Models/Address';
import { User } from 'src/app/Models/User';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-mon-compte',
  templateUrl: './mon-compte.component.html',
  styleUrls: ['./mon-compte.component.scss']
})
export class MonCompteComponent implements OnInit{
  user: User | null = null;
  addresses: Address[] = [];
  constructor (
    private _router: Router,
    protected _userService: UserService,
    private _authService: AuthService
  ) { }

  ngOnInit(): void {
    this._userService.me().subscribe(
      (data: User) => {
        if (data) {
          this.user = data;
          for (const address of data.addresses) {
            this.addresses.push(address);
          }
        }
      }
    )
  }

  editUser(): void {
    if (this.user) {
      this._router.navigate(["user/edit", this.user.id]);
    }
  }

  deleteUser(): void {
    if (this.user) {
      this._userService.deleteUser(this.user.id).subscribe(
        (data: any) => {
          this._authService.logout();
          window.location.reload();
        }
      );
    }
  }
}
