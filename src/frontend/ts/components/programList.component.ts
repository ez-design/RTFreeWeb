import { Component, Input, OnInit } from '@angular/core';
import { AppService } from '../services/app.service';
import { IProgram } from '../interfaces/program.interface';

@Component({
  selector: 'ProgramList',
  template: `
    <form>
      <div class="form-group">
          <label for="pass">日付</label>
          <select name="date" [(ngModel)]="date" (change)="onChangeDate()">
            <option *ngFor="let date of dateList" (value)="date">{{date}}</option>
          </select>
        </div>
    </form>
    <p *ngIf="loading">読み込み中です</p>
    <table class="table" *ngIf="!loading">
      <tr *ngFor="let program of programs">
        <th style="width:80px">{{program.start|date:'HH:mm'}}
        <td>{{program.title}}</td>
        <td class="text-right">
          <button type="button" class="btn btn-default" (click)="onClickRecording(program.id)" *ngIf="program.canRecording">
            <span class="glyphicon glyphicon-play" aria-hidden="true"></span>
          </button>
        </td>
      </tr>
    </table>
  `
})
export class ProgramListComponent implements OnInit{
    @Input()
    public stationId:string;

    @Input()
    public onPlay:(url:string)=>void;

    private programs:IProgram[] = [];
    private date:string;
    private dateList:string[] = [];
    private loading:boolean = false;

    constructor(private appService:AppService){
      
    }

    ngOnInit(){
      let start = new Date();
      start.setDate(start.getDate() -7);
      start.setHours(5);
      start.setMinutes(0);
      start.setSeconds(0);

      let end = new Date();
      end.setDate(end.getDate() + 6 );
      end.setHours(5);
      end.setMinutes(0);
      end.setSeconds(0);

      for(let tmp = start; tmp <= end ; tmp.setDate(tmp.getDate() + 1 )){
        this.dateList.push(tmp.getFullYear() + '-' + ('00' + (tmp.getMonth() + 1)).slice(-2) + '-' + ('00' + tmp.getDate()).slice(-2));
      }
      let today = new Date();
      this.date = today.getFullYear() + '-' + ('00' + (today.getMonth() + 1)).slice(-2) + '-' + ('00' + today.getDate()).slice(-2)
      
      
      this.onChangeDate();
    }

    private onChangeDate = () =>{
      this.loading = true;
      this.appService.getPrograms(this.stationId, this.date).then((programs:IProgram[])=>{
        this.loading = false;
        programs.forEach((p)=>{
          p.canRecording = new Date(p.end) < new Date();
        });
        this.programs = programs;

      });
    }

    /**
     * 録音実行
     */
    private onClickRecording = (program_id) =>{
      this.appService.recording(program_id).then((result) =>{
        if(result.result){
          window.open('./api/library/' + program_id);
        } else if(result.message){
          alert(result.message);
        }
      });
    };

}