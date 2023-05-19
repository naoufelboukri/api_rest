import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerGuard implements CanActivate {
  
  constructor (
    private router: Router
  ) { }

  canActivate( route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
      if (!localStorage.getItem('UserToken')) {
        return true;
      }
      this.router.navigateByUrl('/');
      return false;
  }
  
}
