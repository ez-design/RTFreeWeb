import { Component, OnInit, Input } from '@angular/core';
import { AppService } from '../services/app.service';
import { IStation } from '../interfaces/station.interface';

@Component({
  selector: 'StationList',
  template: `
    <div class="list-group">
      <a href="#" *ngFor="let station of stations" class="list-group-item" (click)="onSelectStation(station.id)">{{station.name}}</a>
    </div>
  `
})
export class StationListComponent implements OnInit{
  @Input()
  public onSelectStation:(station_id:string) => void  = (station_id:string)=>{};


    private stations:IStation[] = [];

    constructor(private appService:AppService){
      
    }

  /**
   * 初期化
   */
    public ngOnInit(){
      this.appService.getStations().then((stations:IStation[])=>{
        this.stations = stations;
      });
    };

}