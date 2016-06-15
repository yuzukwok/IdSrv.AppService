using IdentityServer3.Core.Configuration;
using IdSrv.Lib;
using IdSrv.Lib.Setting;
using Its.Configuration;
using Owin;
using System.Collections.Generic;
using System.Configuration;

namespace IdSrv.App
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            var factory = new IdentityServerServiceFactory();
            var setting = Settings.Get<IdSrvSetting>();
            var options = new IdentityServerOptions()
                .Init(setting.CertFileName, setting.CertFileKeyPwd, factory)
                .UseLog(setting.UseLog);

          
            factory.UseEfStore(setting.EfConnectionString,true);
            factory.ScanUserServiceDLL();         
            appBuilder.UseIdentityServer(options);
            appBuilder.UseIdSrvAdmin();
        }
    }
}
