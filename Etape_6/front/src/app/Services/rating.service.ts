import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Rating } from '../Models/Rating';

@Injectable({
  providedIn: 'root'
})
export class RatingService extends BaseService<Rating>{
  constructor(_http: HttpClient) {
    super(_http, 'rating')
  }
}
