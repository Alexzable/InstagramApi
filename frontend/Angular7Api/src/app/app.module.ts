import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms'

import { AppComponent } from './app.component';
import { UserDetailService } from './shared/user-detail.service';
import {HttpClientModule} from "@angular/common/http";
import { MatButtonModule, MatIconModule, MatListModule, MatToolbarModule, MatInputModule, MatMenuModule, MatDialogModule } from '@angular/material';
import { NoopAnimationsModule, BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserModule,
    MatToolbarModule,
    MatButtonModule,
    MatListModule,
    MatIconModule,
    MatDialogModule,
    MatInputModule,
    BrowserAnimationsModule,
    FormsModule,
    MatMenuModule,
    HttpClientModule
  ],
  providers: [UserDetailService],
  bootstrap: [AppComponent]
})
export class AppModule { }
