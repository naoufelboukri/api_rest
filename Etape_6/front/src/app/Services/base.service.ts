import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { env } from '../env';

@Injectable({
  providedIn: 'root'
})
export class BaseService<T> {
    private API_URL: string = env.API_URL;

    constructor(
        protected _http: HttpClient,
        @Inject(String) protected endpoint: string
    ) { }
    
    getAll() {
        return this._http.get<T[]>(`${this.API_URL}/${this.endpoint}`);
    }

    getOne(id: number) {
        return this._http.get<T>(`${this.API_URL}/${this.endpoint}/${id}`);
    }

    create(item: T) {
        return this._http.post<T>(`${this.API_URL}/${this.endpoint}`, item);
    }

    update(item: T, id: number) {
        return this._http.put<T>(`${this.API_URL}/${this.endpoint}/${id}`, item);
    }

    delete(id: number) {
        return this._http.delete(`${this.API_URL}/${this.endpoint}/${id}`);
    }
}
