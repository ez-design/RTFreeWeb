import { Injectable } from '@angular/core';
import { Http, URLSearchParams, Headers, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import{IStation} from '../interfaces/station.interface';
import{IProgram} from '../interfaces/program.interface';
import{ILibrary} from '../interfaces/library.interface';

@Injectable()
export class AppService {

    constructor(private http: Http) {}

    /**
     * ログイン
     */
    public login = (id:string, pass:string) =>{
        return this.http.post('/api/login/', { id: id, password: pass}).toPromise().then(response => response.json());
    };

    /**
    * 放送局取得
    */
    public getStations = () =>{
        return this.http.get('/api/station/').toPromise().then(response => response.json() as IStation[]);
    };

    /**
     * 放送局再取得
     */
    public refreshStations = () =>{
        return this.http.post('/api/station/', {}).toPromise().then(response => response.json());
    };

    /**
     * 番組表取得
     */
    public getPrograms = (station_id:string, date:string) =>{
        return this.http.get('/api/program/' + station_id + '/' + date).toPromise().then(response => response.json() as IProgram[]);
    };

    /**
     * 録音実行
     */
    public recording = (program_id:string) =>{
        return this.http.get('/api/recording/' + program_id).toPromise().then(response => response.json());
    };

    /**
     * ライブラリ取得
     */
    public getLibaries = (page:number) =>{
        return this.http.get('/api/library/?page=' + page).toPromise().then(response => response.json());
    };

    /**
     * パスワード保存
     */
    public savePassword = (password:string) =>{
        return this.http.post('/api/config/pass/', {password:password}).toPromise().then(response => response.json());
    };

    /**
     * radikoプレミアムログインチェック
     */
    public checkRadikoLogin =  (mail:string, password:string) =>{
        return this.http.post('/api/config/check/', {radikoEmail: mail, radikoPassword:password}).toPromise().then(response => response.json());
    };

    /**
     * radikoプレミアム設定保存
     */
    public saveRadikoLogin = (mail:string, password:string) =>{
        return this.http.post('/api/config/radiko',  { radikoEmail: mail, radikoPassword:password}).toPromise().then(response => response.json());
    };

    /**
     * ログアウト処理
     */
    public logout = () =>{
        return this.http.get('/api/logout/').toPromise().then(response => response.json());
    };

}