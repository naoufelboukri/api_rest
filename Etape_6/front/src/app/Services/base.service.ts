import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { env } from '../env';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { Meta } from '../Models/Meta';

@Injectable({
  providedIn: 'root'
})

class Response<T> {
    data: T[];
    meta: Meta
}

export class BaseService<T> {
    protected API_URL: string = env.API_URL;

    constructor(
        private router: Router,
        protected _http: HttpClient,
        @Inject(String) protected endpoint: string,
    ) { }
    
    getAll(PageNumber: number = 1, PageSize: number = 5) {
        return this._http.get<Response<T>>(`${this.API_URL}/${this.endpoint}?PageSize=${PageSize}&PageNumber=${PageNumber}`);
    }

    getOne(id: number) {
        return this._http.get<T>(`${this.API_URL}/${this.endpoint}/${id}`);
    }

    create(item: T) {
        return this._http.post<T>(`${this.API_URL}/${this.endpoint}`, item);
    }

    update(item: any, id: number) {        
        return this._http.put<T>(`${this.API_URL}/${this.endpoint}/${id}`, item);
    }

    delete(id: number) {
        return this._http.delete(`${this.API_URL}/${this.endpoint}/${id}`);
    }

    private handleError(err: any): void {
        if (err.error = "Token vide ou invalide") {
            localStorage.removeItem('UserToken');
            this.router.navigate(['unauthorize']);
        }
    }   
}
