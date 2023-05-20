import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Rating } from '../Models/Rating';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RatingService extends BaseService<Rating>{
  constructor(_http: HttpClient, router: Router) {
    super(router, _http, 'rating')
  }
}
