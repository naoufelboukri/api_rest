import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Address } from 'src/app/Models/Address';
import { User } from 'src/app/Models/User';
import { AddressService } from 'src/app/Services/address.service';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {

  user: User | null = null;
  addresses: Address[] = [];
  isAdmin: boolean = false;
  constructor (
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _addressService: AddressService,
    private _router: Router
  ) { }

  ngOnInit(): void {
    const userId: string | null = this._route.snapshot.paramMap.get('id');
    if (userId) {
      this._userService.getUser(+userId).subscribe(
        (data: User) => {
          console.log(data);
          this.user = data;
          for (const address of data.addresses) {
            this.addresses.push(address);
          }
        }
      )
    }

    this._userService.me().subscribe(
      data => {
        if (data.role === 'ROLE_ADMIN') {
          this.isAdmin = true;
        }
      }
    )
  }

  editUser() {
    if (this.isAdmin) {      
      this._router.navigate(["user/edit", this._route.snapshot.paramMap.get('id')]);
    }
  }
}
