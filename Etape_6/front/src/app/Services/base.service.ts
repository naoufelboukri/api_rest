import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { env } from '../env';
import { Router } from '@angular/router';
import { catchError, of, tap } from 'rxjs';

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
    
    getAll(page: number = 1) {
        return this._http.get<T[]>(`${this.API_URL}/${this.endpoint}?page=${page}`);
    }

    getOne(id: number) {
        return this._http.get<T>(`${this.API_URL}/${this.endpoint}/${id}`);
    }

    create(item: T) {
        return this._http.post<T>(`${this.API_URL}/${this.endpoint}`, item).pipe(
            tap(
                () => {

                },
                err => {
                    this.handleError(err);
                }
            )
        );
    }

    update(item: T, id: number) {
        return this._http.put<T>(`${this.API_URL}/${this.endpoint}/${id}`, item).pipe(
            tap(
                () => {

                },
                err => {
                    this.handleError(err);
                }
            )
        );
    }

    delete(id: number) {
        return this._http.delete(`${this.API_URL}/${this.endpoint}/${id}`).pipe(
            tap(
                () => {

                },
                err => {
                    this.handleError(err);
                }
            )
        );
    }

    private handleError(err: any): void {
        if (err.error = "Token vide ou invalide") {
            localStorage.removeItem('UserToken');
            this.router.navigate(['unauthorize']);
        }
    }
}
