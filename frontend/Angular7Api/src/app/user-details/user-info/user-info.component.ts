import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserDetailService } from 'src/app/shared/user-detail.service';
import { NgForm } from '@angular/forms';
import { UserDetail } from 'src/app/shared/user-detail.model';
import { Observable } from 'rxjs';
import { ResponseContentType } from '@angular/http';


@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styles: []
})
export class UserInfoComponent implements OnInit {
  toastr: any;
  http: any;
  constructor(private service: UserDetailService) { }
  client_Secret: string = "";
  full_name : string ="";
  cient_Id : string =""
  image_url: string="https://scontent.cdninstagram.com/vp/1ee2eda0555c50100ee5316c4f593375/5E180C47/t51.2885-19/s150x150/71013329_2335946206656076_4457621590149955584_n.jpg?_nc_ht=scontent.cdninstagram.com";
 
  ngOnInit() {
   
  }
 
  getImage(imageUrl: string): Observable<File> {
    let headers={ responseType: ResponseContentType.Blob }
    return this.http
        .get(imageUrl, headers)
        .map((res: Response) => res.blob());
  } 


  loadData(form:UserDetail){
    this.service.formData = Object.assign({}, form);

    this.full_name = this.service.formData.Name;
    this.image_url = this.service.formData.ProfilePicture;
    this.cient_Id = this.service.formData.ClientId;
    this.client_Secret =this.service.formData.ClientSecret;
  }
}
