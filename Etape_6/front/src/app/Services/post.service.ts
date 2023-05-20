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
}