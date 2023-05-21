import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Post } from '../Models/Post';
import { Router } from '@angular/router';
import { catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostService extends BaseService<Post>{
  constructor(router: Router, _http: HttpClient) {
    super(router, _http, 'posts')
  }

  getBySearch(page: number = 1, search: string = '') {
    return this._http.get<Post[]>(`${this.API_URL}/${this.endpoint}/search?page=${page}&search=${search}`);
}
}