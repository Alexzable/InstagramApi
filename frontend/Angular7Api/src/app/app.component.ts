import { Component, OnInit, Injectable } from '@angular/core';
import { UserDetailService } from './shared/user-detail.service';
import { UserDetail } from './shared/user-detail.model';
import { NgForm } from '@angular/forms';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styles: []
})
export class AppComponent implements OnInit{

  title = 'Angular7Api';
  dataReceived : boolean =false;
  public user: UserDetail;
  public safeHtml: SafeHtml;
  
  constructor(
    public service: UserDetailService,
    private sanitizer: DomSanitizer
  ){}

  ngOnInit(): void {
    this.user = {
      Username: '',
      Password: '',
      ClientId: '',
      ClientSecret: '',
      Name: '',
      ProfePicture: '',
      LinkBio: '',
      Comments: 0,
      Likes: 0,
      IDInsta: 0,
      Location: '',
      Html: '',
      Media: 0,
      Followers: 0,
      FollowedBy: 0

    }
    
  }

 
  openUser(form: NgForm): void {
   
    this.service.getUser(this.user.Username, this.user.Password)
        .subscribe(
            (res: UserDetail) => {
                console.log('data', res);
                this.dataReceived = true;
                this.user = Object.assign({}, res);
                this.safeHtml = this.sanitizer.bypassSecurityTrustHtml(this.user.Html);
            },
            (error: any) => console.log(error),
            ()=> console.log('completed')
        );
  } 
  
 
}
