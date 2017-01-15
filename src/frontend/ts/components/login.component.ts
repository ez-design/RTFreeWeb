import { Component, Input } from '@angular/core';
import { AppService } from '../services/app.service';

@Component({
  selector: 'Login',
  template: `<form (submit)="onSubmit()" style="margin-top:50px">
    <div class="panel panel-default">
      <div class="panel-body">
        <div class="form-group">
          <label for="id">ID</label>
          <input type="text" class="form-control" id="id" placeholder="ID" name="id" [(ngModel)]="id">
        </div>
        <div class="form-group">
          <label for="pass">パスワード</label>
          <input type="password" class="form-control" id="pass" placeholder="パスワード" name="pass" [(ngModel)]="pass">
        </div>
        <input type="submit" value="ログイン" class="btn btn-primary btn-block" />
      </div>
    </div>
  </form>`
})
export class LoginComponent{
    private id:string;
    private pass:string;

    @Input()
    public onLogin:() => void;

    constructor(private appService:AppService){
    }

    public onSubmit = () =>{
        console.log(this.id + ',' + this.pass);
        this.appService.login(this.id, this.pass).then((res) =>{
          if(res.result && this.onLogin){
            this.onLogin();
          } else if(!res.result){
            alert(res.message);
          }
        });

    };


}