import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Post } from '../Models/Post';

@Injectable({
  providedIn: 'root'
})
export class PostService extends BaseService<Post>{
  constructor(_http: HttpClient) {
    super(_http, 'posts')
  }
}