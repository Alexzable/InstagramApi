import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';

import { of } from 'rxjs/observable/of';

import { catchError, map, tap } from 'rxjs/operators';

import { BaseService } from './base.service';

import { User } from '../models/user';

import { AppConfig } from '../config/config';

import { Helpers } from '../helpers/helpers';

@Injectable()

export class UserService extends BaseService {

  private pathAPI = this.config.setting['PathAPI'];

  constructor(private http: HttpClient, private config: AppConfig, helper: Helpers) { super(helper); }

  /** GET heroes from the server */

  getUsers (): Observable<User[]> {

   // return this.http.get<(this.pathAPI + 'user', super.header()).pipe(
       // catchError(super.handleError));
}
