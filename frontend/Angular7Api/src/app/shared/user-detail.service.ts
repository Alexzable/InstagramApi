import { Injectable } from '@angular/core';
import { UserDetail } from './user-detail.model';
import {HttpClient, HttpParams} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserDetailService {
  formData: UserDetail
  readonly rootURL ='https://localhost:44336/api';
  
  constructor(private http:HttpClient) { }

  getUserAccount(formData:UserDetail){
    const params = new HttpParams()
    .set('Username', formData.Username)
    .set('Password', formData.Password);

    return this.http.get(this.rootURL + '/Users/'+ formData.Username+"/"+formData.Password, { params })
    .toPromise()
    .then(res=> this.formData = res as UserDetail);
    
  }
  
}
