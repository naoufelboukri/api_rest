import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { User } from '../Models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService<User>{
  constructor(_http: HttpClient) {
    super(_http, 'users')
  }
}
