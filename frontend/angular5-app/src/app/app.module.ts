import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

import {MatButtonModule, MatCheckboxModule, AUTOCOMPLETE_PANEL_HEIGHT} from '@angular/material';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSidenavModule} from '@angular/material/sidenav';
import { HttpClientModule } from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import { AppComponent } from './layout/app.component';
import { HeadComponent } from './layout/head.component';
import { LeftPanelComponent } from './layout/left-panel.component';

@NgModule({
  declarations: [
    AppComponent,
    HeadComponent,
    LeftPanelComponent
  ],
  imports: [
    BrowserModule,
    
    AppRoutingModule,

    BrowserAnimationsModule,

    MatButtonModule, 

    MatCheckboxModule,

    MatInputModule,

    MatFormFieldModule,

    MatSidenavModule,

    AppRoutingModule,

    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
