import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, throwError } from "rxjs";
import { AuthenticationService } from "./Services/authentication.service";
import { Router } from "@angular/router";
import { env } from "./env";

@Injectable()

export class JwtInterceptor implements HttpInterceptor {
    constructor(
        private _authService: AuthenticationService,
        private router: Router
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let token = this._authService.userSubject.value;
        if (token) {
            req = req.clone({
                headers: new HttpHeaders({
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                })
            })
        }
        return next.handle(req)
        .pipe(
            catchError((error: any) => {
                if (error.status === 401 && error.url != `${env.API_URL}/me`) {      
                    this._authService.logout();
                }
                return throwError('Sessions expired');
            })
        );
    }
}