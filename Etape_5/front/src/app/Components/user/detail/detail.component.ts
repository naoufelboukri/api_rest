import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route } from '@angular/router';
import { Address } from 'src/app/Models/Address';
import { User } from 'src/app/Models/User';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {

  user: User | null = null;
  addresses: Address[] = [];
  
  constructor (
    private _route: ActivatedRoute,
    private _userService: UserService,
  ) { }

  ngOnInit(): void {
    const userId: string | null = this._route.snapshot.paramMap.get('id');
    if (userId) {
      this._userService.getUser(+userId).subscribe(
        (data: User) => {
          console.log(data);
          this.user = data;
        }
      )
    }
  }

}
