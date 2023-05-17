import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthenticationService } from "./Services/authentication.service";

@Injectable()

export class JwtInterceptor implements HttpInterceptor {
    constructor(
        private _authService: AuthenticationService
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
        return next.handle(req);
    }
}