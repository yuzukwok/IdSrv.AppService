# IdSrv.AppService
基于IdentityServer3搭建的用户认证服务器（Mysql，带管理界面，无具体业务关联）

![image](https://github.com/yuzukwok/IdSrv.AppService/blob/master/screenshot/admin1.png)

#技术路线
基于IdentityServer3，IdentityServer3.Admin,IdentityServer3.Ef 集成而成，配置文件lts.configuran 类库读取json文件，可自动区分环境,针对Mysql进行修改处理


#使用
配置文件说明  
```
{    
  "Port": 9000, //端口号  
  "CertFileName": " ",//证书文件 pfx结尾  
  "CertFileKeyPwd": " ",//证书文件密码  
  "UseLog": true,//是否使用日志，日志使用NLog类库  

  "EfConnectionString": "Default", //EF数据库连接  
  "UserServiceMappings": [ //UserService配置，针对一个ClienId 设置具体的类（必须实现IUserService接口），实现DLL放到bin目录  
    {
      "ClientId": "appclientvr",    
      "UserServiceClassFullName": " ",  
      "Extparam": "Default"  //实现类 支持无参数或一个string参数的构造子  
    }  
  ]  
} 
```

1. 使用makecert 创建 密钥证书 并设定密码 导出为pfx格式
2. 使用IdSrvMysqlsql.txt语句建表  
3. 创建一个DLL 实现IUserService接口，实现对用户的授权的认证判断
4. 修改配置文件 UserService配置可先忽略（配置文件读取规则 请参考Its.Configuration说明）
5. 启动IdSrvApp.exe 访问管理网站 默认 http://localhost:9000/admin  按照需求创建Client和Scope
6. 停止IdSrvApp,拷贝DLL到bin目录，配置配置文件，针对某个类型的ClientId 对应一个UserService类 ,进行特定模块的用户认证。
7. 重启，Enjoy

整个平台主要维护Client，SCope等数据，基于IdentityServer3 可以实现OAuth，OpenId 等各种标准认证接口，具体用户验证还是通过业务系统。

#第三方类库  
https://github.com/yuzukwok/IdentityServer3.EntityFramework  IdentityServer3.EntityFramework  mysql-Ver 分支
https://github.com/yuzukwok/IdentityServer3.Admin.EntityFramework IdentityServer3.Admin.EntityFramework mysql-Ver 分支
https://github.com/IdentityServer/IdentityServer3 IdentityServer3
https://github.com/Topshelf/Topshelf  Topshelf
https://github.com/jonsequitur/Its.Configuration  Its.Configuration
 
