import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../Services/authentication.service';
import { UserService } from '../Services/user.service';

@Injectable({
  providedIn: 'root'
})

export class PersonalGuard implements CanActivate {
  constructor (
    private _router: Router,
    private _authService: AuthenticationService,
    private _userService: UserService
  ) { }

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {
    return new Observable(
      (observer) => {
        return new Observable<boolean>(
          profile => {
            this._authService.me().subscribe(
              data => {
                const id = data.id;
                const postId = route.paramMap.get('id');
                if (postId) {
                  this._userService.getOne(id).subscribe(
                    user => {
                      if (user.posts.find(p => p.id === +postId) || user.role === 'ROLE_ADMIN') {
                        profile.next(true);
                      }
                      profile.next(false);
                    },
                    err => {
                      profile.next(false);
                    }
                  ) 
                }
              },
              (err) => {
                profile.next(false);
              }
            )
          }
        ).subscribe(
          result => {
            if (result) {
              observer.next(result);
            } else {
              this._router.navigate(['unauthorize']);
              observer.next(false);
            }
          },
          err => {
            this._router.navigate(['page-not-found']);
            observer.error(err);
          }
        )
      }
    )
  }
}
