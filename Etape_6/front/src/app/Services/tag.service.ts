import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Tag } from '../Models/Tag';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class TagService extends BaseService<Tag>{
  constructor(router: Router, _http: HttpClient) {
    super(router, _http, 'tags')
  }
}
