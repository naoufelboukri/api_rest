import { Injectable } from '@angular/core';
import { env } from '../env';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from '../Models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private API_URL = env.API_URL;

  public user: Observable<string>;
  public userSubject: BehaviorSubject<string>;
  public isLogged: boolean = (localStorage.getItem('UserToken') !== null) ? true : false;
  
  constructor(
    private _http: HttpClient,
    private _router: Router
  ) {
    this.userSubject = new BehaviorSubject<string>(localStorage.getItem('UserToken') || '');
    this.user = this.userSubject.asObservable();
  }

  login(username: string, password: string) {
    return this._http.post(`${this.API_URL}/authenticate`, {username: username, password: password}).pipe(
      tap((user: any) => {
        if (user) {
          this.userSubject.next(user);
        }
      })
    );
  }

  logout() {
    this._router.navigate(['']);
    localStorage.removeItem('UserToken');
    window.location.reload();
  }

  register(username: string, password: string) {
    return this._http.post(`${this.API_URL}/register`, {username: username, password: password});
  }

  me() {
    return this._http.get<User>(`${this.API_URL}/me`);
  }
}
