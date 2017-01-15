import { Component, OnInit } from '@angular/core';
import { AppService } from '../services/app.service';
import { IConfig } from '../interfaces/config.interface';

@Component({
  selector: 'Config',
  template: `
    <div class="panel panel-default">
      <div class="panel-heading">システム設定</div>
      <div class="panel-body">
        <form (submit)="onSubmitPassword()">
          <div class="form-group">
            <label for="pass">パスワード<br />※変更する場合のみ入力してください</label>
            <input type="password" class="form-control" id="pass" placeholder="パスワード" name="pass" [(ngModel)]="config.password">
          </div>
          <button type="submit" class="btn btn-primary btn-block">保存</button>
        </form>
      </div>
    </div>

    <div class="panel panel-default">
      <div class="panel-heading">radikoプレミアム設定</div>
      <div class="panel-body">
        <form (submit)="onSubmitRadiko()">
          <div class="form-group">
            <label for="radikoMail">radikoプレミアムメールアドレス<br />※変更する場合のみ入力してください</label>
            <input type="text" class="form-control" id="radikoMail" placeholder="radikoプレミアムメールアドレス" name="radikoMail" [(ngModel)]="config.radikoMail">
          </div>
          <div class="form-group">
            <label for="radikoPass">radikoプレミアムパスワード<br />※変更する場合のみ入力してください</label>
            <input type="password" class="form-control" id="radikoPass" placeholder="radikoプレミアムパスワード" name="radikoPass" [(ngModel)]="config.radikoPass">
          </div>
          <div class="row">
            <div class="col-xs-6">
              <button type="button" class="btn btn-default btn-block" (click)="onClickTestLogin()">テストログイン</button>
            </div>
            <div class="col-xs-6">
              <button type="submit" class="btn btn-primary btn-block">保存</button>
            </div>
          </div>
        </form>
      </div>
    </div>

    <div class="panel panel-default">
      <div class="panel-heading">放送局設定</div>
      <div class="panel-body">
        <button type="button" class="btn btn-default btn-block" (click)="onClickRefreshStationList()">放送局一覧再取得</button>
      </div>
    </div>
    
  `
})
export class ConfigComponent implements OnInit{
    private config:IConfig = { };
    constructor(private appService:AppService){

    }

    ngOnInit(){

    }

    /**
     * radikoプレミアムにテストログイン
     */
    private onClickTestLogin = () =>{
        this.appService.checkRadikoLogin(this.config.radikoMail, this.config.radikoPass).then((result) =>{
          if(result.result){
            alert('ログインに成功しました');
          } else if(result.message) {
            alert(result.message);
          } else {
            alert('ログインに失敗しました');
          }
        });
    };

    /**
     * 放送局一覧再取得
     */
    private onClickRefreshStationList = () =>{
      this.appService.refreshStations().then((result)=>{
        if(result.result){
          alert('再取得しました');
        }
      });
    };

    /**
     * パスワード保存
     */
    private onSubmitPassword = () =>{
      console.log(this.config)
      this.appService.savePassword(this.config.password).then((result) =>{
        if(result.result){
            alert('パスワードを変更しました');
          } else if(result.message) {
            alert(result.message);
          }
      });
    };

    /**
     * radikoアカウント保存
     */
    private onSubmitRadiko = () =>{
      this.appService.saveRadikoLogin(this.config.radikoMail, this.config.radikoPass).then((result) =>{
        if(result.result){
            alert('radikoプレミアム設定を保存しました');
          }
      });
    };

}