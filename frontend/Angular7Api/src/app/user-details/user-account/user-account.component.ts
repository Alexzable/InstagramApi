import { Component, OnInit, Input } from '@angular/core';
import { UserDetailService } from 'src/app/shared/user-detail.service';
import { NgForm } from '@angular/forms';
import { stringify } from 'querystring';
import { UserDetail } from 'src/app/shared/user-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styles: []
})
export class UserAccountComponent implements OnInit {
  constructor(private service: UserDetailService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?:NgForm){
    if(form!=null)
      form.resetForm();
    this.service.formData = {
      Username: '',
      Password: '',
      ClientId: '',
      ClientSecret: '',
      Name: '',
      ProfilePicture: '',
      LinkBio: '',
      Comment: '',
      Likes: '',
      IDInsta: 0,
      Location: '',
      ID: 0
    }
  }

  onSubmit(form:NgForm){
    this.login(form);
  }

  login(form:NgForm){
    const UserBecome = this.service.formData;
      
    this.service.getUserAccount(UserBecome).then(function(user){
      console.log(user);
      //this.loadData(user);
    });
  }
 
}