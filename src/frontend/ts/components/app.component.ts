import { Component } from '@angular/core';
import { AppService } from '../services/app.service';

@Component({
  selector: 'App',
  template: `
    <div id="app">
      <nav class="navbar navbar-inverse" *ngIf="isLogin" >
        <div class="container">
          <button type="button" (click)="isMenuOpen = true;" class="btn btn-default navbar-btn">
            <span class="glyphicon glyphicon-menu-hamburger" aria-hidden="true"></span>
          </button>
        </div>
      </nav>
      <div class="container">
        <Login *ngIf="!isLogin" [onLogin]="onLogin"></Login>
        <StationList [onSelectStation]="onSelectStation" *ngIf="selectedTool=='stationList'"></StationList>
        <ProgramList [stationId]="stationId" *ngIf="selectedTool=='programList'" [onPlay]="onPlay"></ProgramList>
        <LibraryList *ngIf="selectedTool=='libraryList'"></LibraryList>
        <Config *ngIf="selectedTool=='config'"></Config>
      </div>
      <div id="menu" [class.open]="isMenuOpen" (click)="isMenuOpen = false;">
        <div>
          <ul>
            <li (click)="selectedTool = 'stationList'">
              <span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>放送局
            </li>
            <li (click)="selectedTool = 'libraryList'">
              <span class="glyphicon glyphicon-headphones" aria-hidden="true"></span>ライブラリ
            </li>
            <li (click)="selectedTool = 'config'">
              <span class="glyphicon glyphicon-cog" aria-hidden="true"></span>設定
            </li>
            <li (click)="onClickLogout()">
              <span class="glyphicon glyphicon-off" aria-hidden="true"></span>ログアウト
            </li>
          </ul>
        </div>
      </div>
    </div>
  `,
  providers: [ AppService ]
})
export class AppComponent{
  private stationId:string;
  private isLogin:boolean = false;

  private isMenuOpen:boolean = false;
  private selectedTool:string = '';

  public constructor(private appService: AppService){

  }


  public onSelectStation = (station_id:string) =>{
    this.selectedTool = 'programList';
    this.stationId = station_id;
  };

  private onLogin = (res) =>{
    this.isLogin = true;
    this.selectedTool = 'stationList';
  };

  /**
   * ログアウト
   */
  private onClickLogout = () =>{
      if(confirm('ログアウトしますか？')){
        this.appService.logout().then(() =>{
          this.isLogin = false;
          this.selectedTool = '';
        });
      }
  };

}