# RTFreeWeb
RTFreeをWebで操作するフロントエンド

## 必要なもの
- [.NET Core SDK](https://go.microsoft.com/fwlink/?LinkID=835014)
- [mysql](https://www-jp.mysql.com/)
- [RTFree](https://github.com/ez-design/RTFree) (ffmpeg, swfextractも含む)

## 使用方法
1. mysqlにデータベースを作成する
2. 添付のcreate.sqlを実行し、テーブルを作成する
3. appsetting.jsonのConnectionStringを変更する
4. RTFreeを発行し、appsetting.jsonのRTFreePathを変更する
5. RTFreeWebを発行し、実行する
6. localhost:5000にアクセスし、ユーザー作成を行う

## 仕様など
- ユーザー作成が必須ですが、マルチユーザーではありません
- 使用ポートは5000です
- 環境変数**RTFreeWeb_ConnectionString**はmysqlへの接続文字列です  
これが指定されている場合、appsetting.jsonのConnectionStringは無視されます
- 環境変数**RTFreeWeb_RTFreePath**はRTFree.dllのパスです  
これが指定されている場合、appsetting.jsonのRTFreePathは無視されます  
- UIはスマホでの操作を想定して作成しています


## 推奨導入方法
手軽に導入できるようにDockerイメージも作成しております。  
- [RTFreeWebDocker(GitHub)](https://github.com/ez-design/RTFreeWebDocker)
- [ezdesign/rtfreeweb(DockerHub)](https://hub.docker.com/r/ezdesign/rtfreeweb/)

RTFreeWebDockerのdocker-compose.ymlを使用すれば、mysqlの設定など全て自動で行います。  
また、使用ポートの変更も可能です。