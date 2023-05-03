import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from '../env';
import { User } from '../Models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private API_URL = env.API_URL;

  constructor(
    private _http: HttpClient
  ) { }

  me() {
    return this._http.get<User>(`${this.API_URL}/me`);
  }

  getUsers() {
    return this._http.get<User[]>(`${this.API_URL}/user`);
  }

  getUser(id: number) {
    return this._http.get<User>(`${this.API_URL}/user/${id}`);
  }
}
