import { Injectable } from '@angular/core';
import { env } from '../env';
import { HttpClient } from '@angular/common/http';
import { Address } from '../Models/Address';
import { changeProperty } from '../Utils/functions';

@Injectable({
  providedIn: 'root'
})
export class AddressService {
  private API_URL = env.API_URL;

  constructor(
    private _http: HttpClient
  ) { }

  getAddresses() {
    return this._http.get<Address[]>(`${this.API_URL}/address`);
  }

  getAddress(id: number) {
    return this._http.get<Address>(`${this.API_URL}/address/${id}`);
  }

  newAddress(address: Address) {
    return this._http.post<Address>(`${this.API_URL}/address/`, changeProperty(address, 'road', 'street'));
  }

  editAddress(object: any, id: number) {
    return this._http.put<Address>(`${this.API_URL}/address/${id}`, changeProperty(object, 'road', 'street'));
  }

  deleteAddress(id: number) {
    return this._http.delete(`${this.API_URL}/address/${id}`);
  }
}
