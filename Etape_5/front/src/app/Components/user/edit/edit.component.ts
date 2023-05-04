import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Address } from 'src/app/Models/Address';
import { User } from 'src/app/Models/User';
import { AddressService } from 'src/app/Services/address.service';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { removeEmptyProperty } from 'src/app/Utils/functions';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit{
  user: User | null = null;
  addresses: Address[] = [];
  size: number;
  states: any[] = [
    {name: 'ROLE_USER', abbrev: 'Utilisateur'},
    {name: 'ROLE_ADMIN', abbrev: 'Admministrateur'}
  ];
  error: string = "";

  constructor (
    protected _userService: UserService,
    private _addressService: AddressService,
    private _authService: AuthService,
    private _router: Router,
    private _route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const id = this._route.snapshot.paramMap.get('id');
    if (id) {
      this._userService.getUser(+id).subscribe(
        data => {
          this.user = data;
          for (const address of data.addresses) {
            this.addresses.push(address);
          }
        }
      )
    }
  }

  onSubmit(form: NgForm) {
    const input = removeEmptyProperty(form.value);
    if (this.user) {
      const id = this._route.snapshot.paramMap.get('id');
      if (id) {
        this._userService.editUser(+id, input).subscribe(
          (data: any) => {
            this._authService.logout();
          }
          );
        }
    }
  }

  deleteAddress(id: number) {
    if (id === undefined) {
      this.addresses.splice(this.addresses.findIndex(a => a.id === undefined), 1);
    } else {
      this._addressService.deleteAddress(id).subscribe(
        (data: any) => {
          const address = this.addresses.findIndex((item) => item.id === id);
          if (address !== -1) {
            this.addresses.splice(address, 1);
          }
        }
      )  
    }
  }

  addAddress() {
    const address = new Address();
    address.city = '';
    address.country = '';
    address.postalCode = '';
    address.road = '';
    this.addresses.push(address);
  }

  saveAddress(form: NgForm, address: Address) {    
    if (address.id === undefined) {
      if (!form.valid) {
        this.error = "Vous devez compléter tous le formulaire";
      } else {
        const input: Address = new Address;
        input.city = form.value.city; 
        input.country = form.value.country; 
        input.postalCode = form.value.postalCode; 
        input.road = form.value.road; 
        this._addressService.newAddress(input).subscribe(
          (data: any) => {
            window.location.reload();
          }
        )
      }
    } else {
      const values = removeEmptyProperty(form.value);
      this._addressService.editAddress(values, address.id).subscribe(
        (data: any) => {
          console.log('update');
          window.location.reload();
        }
      )
    }
  }
}
