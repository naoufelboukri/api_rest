import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../Services/authentication.service';
import { UserService } from '../Services/user.service';
import { User } from '../Models/User';

@Injectable({
  providedIn: 'root'
})

export class PersonalGuard implements CanActivate {
  constructor (
    private _router: Router,
    private _authService: AuthenticationService,
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return new Observable(
      (observer) => {
        return new Observable<boolean>(
          profile => {
            this._authService.me().subscribe(
              data => {
                const myId = data.id;
                const routeId = route.paramMap.get('id');
                if (routeId && (+routeId === myId || data.role === 'ROLE_ADMIN')) {
                  profile.next(true);
                } else {
                  profile.error(false);
                }
                profile.complete();
              },
              (err) => {
                profile.error(false);
              }
            )
          }
        ).subscribe(
          profile => {
            observer.next(profile);
          },
          err => {
            this._router.navigate(['']);
          }
        )
      }
    )
  }
}
