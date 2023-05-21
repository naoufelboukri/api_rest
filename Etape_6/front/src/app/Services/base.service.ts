import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { env } from '../env';
import { Router } from '@angular/router';
import { catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BaseService<T> {
    private API_URL: string = env.API_URL;

    constructor(
        private router: Router,
        protected _http: HttpClient,
        @Inject(String) protected endpoint: string,
    ) { }
    
    getAll() {
        return this._http.get<T[]>(`${this.API_URL}/${this.endpoint}`);
    }

    getOne(id: number) {
        return this._http.get<T>(`${this.API_URL}/${this.endpoint}/${id}`);
    }

    create(item: T) {
        console.log(item);
        return this._http.post<T>(`${this.API_URL}/${this.endpoint}`, item).pipe(
            catchError(err => {
                console.log(err);
                // this.handleError(err);
                return of(null);
            })
        );
    }

    update(item: T, id: number) {
        return this._http.put<T>(`${this.API_URL}/${this.endpoint}/${id}`, item).pipe(
            catchError(err => {
                console.log(err);
                // this.handleError(err);
                return of(null);
            })
        );
    }

    delete(id: number) {
        return this._http.delete(`${this.API_URL}/${this.endpoint}/${id}`).pipe(
            catchError(err => {
                console.log(err);
                // this.handleError(err);
                return of(null);
            })
        );
    }

    private handleError(err: any): void {
        // this.router.navigate(['']);
        // localStorage.removeItem('UserToken');
        // window.location.reload();
    }
}
