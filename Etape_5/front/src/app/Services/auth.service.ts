import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { env } from '../env';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

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

  login(email: string, password: string) {
    return this._http.post<string>(`${this.API_URL}/authenticate`, {email: email, password: password}).pipe(
      tap((user: string) => {
        if (user) {
          this.userSubject.next(user);
        }
      })
    )
  }
}
