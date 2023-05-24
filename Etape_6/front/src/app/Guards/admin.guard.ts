import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../Services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor (
    private _router: Router,
    private _authService: AuthenticationService
  ) { }

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {
    return new Observable(
      observer => {
        return new Observable<boolean>(
          (profile) => {
            this._authService.me().subscribe(
              data => {
                profile.next(data.role === 'ROLE_ADMIN');
              },
              err => profile.next(false)
            )
          }
        ).subscribe(
          result => {
            if (result) {
              observer.next(result);
            } else {
              this._router.navigate(['page-not-found']);
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
