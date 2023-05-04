import { Component, OnInit } from '@angular/core';
import { Address } from 'src/app/Models/Address';
import { User } from 'src/app/Models/User';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit{
  user: User | null = null;
  addresses: Address[] = [];

  constructor (
    protected _userService: UserService,
  ) { }

  ngOnInit(): void {
    this._userService.me()
      .subscribe(
        (data: User) => {
          this.user = data;
          for (const address of data.addresses) {
            this.addresses.push(address);
          }
        }
      )   
  }

  editUser() {

  }
}
