import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Tag } from '../Models/Tag';

@Injectable({
  providedIn: 'root'
})
export class TagService extends BaseService<Tag>{
  constructor(_http: HttpClient) {
    super(_http, 'tags')
  }
}
