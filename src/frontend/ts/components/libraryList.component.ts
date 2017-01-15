import { Component, Input, SimpleChange } from '@angular/core';
import { AppService } from '../services/app.service';
import { ILibrary } from '../interfaces/library.interface';

@Component({
  selector: 'LibraryList',
  template: `
    <div class="row">
      <div class="col-xs-6">
        <button type="button" class="btn btn-default" *ngIf="page > 1" (click)="setPage(page-1)">
          <span class="glyphicon glyphicon-menu-left" aria-hidden="true"></span>
        </button>
      </div>
      <div class="col-xs-6 text-right">
        <button class="btn btn-default" *ngIf="page < maxPage" (click)="setPage(page+1)">
          <span class="glyphicon glyphicon-menu-right" aria-hidden="true"></span>
        </button>
      </div>
    </div>
    <table class="table" style="margin-top:20px;">
      <tr *ngFor="let library of libraries">
        <th style="width:140px">{{library.program.start|date:'yyyy/MM/dd HH:mm'}}</th>
        <td>{{library.program.title}}</td>
        <td class="text-right">
          <button type="button" class="btn btn-default" (click)="onClickPlay(library.program.id)">
            <span class="glyphicon glyphicon-play" aria-hidden="true"></span>
          </button>
        </td>
      </tr>
    </table>
  `
})
export class LibraryListComponent{
    private libraries:ILibrary[] = [];
    private page:number = 1;
    private maxPage:number;
    private player;

    constructor(private appService:AppService){
      this.setPage(1);
      
    }

    /**
     * 再生
     */
    private onClickPlay = (program_id:string) =>{
      window.open('./api/library/' + program_id);
    };

    /**
     * ページ移動
     */
    private setPage = (page:number) =>{
      this.page = page;
      this.appService.getLibaries(page).then((result) =>{
        this.libraries = result.data;
        this.maxPage = result.maxPage;
      });
    };

}