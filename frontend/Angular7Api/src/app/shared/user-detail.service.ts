import { Injectable } from '@angular/core';
import { UserDetail } from './user-detail.model';
import {HttpClient, HttpParams} from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserDetailService {
 
  readonly rootURL ='https://localhost:44336/CallBack/PassDataToAngular';

  
  constructor(private http:HttpClient) { }

  getUser(Username:string, Password: string, ChosenPhoto: number) : Observable<UserDetail>
  {
    const params = new HttpParams()
    .set('Username', Username)
    .set('Password', Password)
    .set('ChosenPhoto',ChosenPhoto.toString())

    return this.http.get<UserDetail>(this.rootURL, { params });
    
  }
  
}
