import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms'

import { AppComponent } from './app.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserAccountComponent } from './user-details/user-account/user-account.component';
import { UserDetailService } from './shared/user-detail.service';
import {HttpClientModule} from "@angular/common/http";
import { UserInfoComponent } from './user-details/user-info/user-info.component';

@NgModule({
  declarations: [
    AppComponent,
    UserDetailsComponent,
    UserAccountComponent,
    UserInfoComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [UserDetailService],
  bootstrap: [AppComponent]
})
export class AppModule { }
