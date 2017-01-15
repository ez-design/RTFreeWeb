import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';


import { AppComponent }  from './components/app.component';
import { LoginComponent }  from './components/login.component';
import { StationListComponent }  from './components/stationList.component';
import { ProgramListComponent }  from './components/programList.component';
import { LibraryListComponent }  from './components/libraryList.component';
import { ConfigComponent }  from './components/config.component';

@NgModule({
  imports:      [ BrowserModule, FormsModule, HttpModule ],
  declarations: [ 
    AppComponent,
    LoginComponent,
    StationListComponent,
    ProgramListComponent,
    LibraryListComponent,
    ConfigComponent
  ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }